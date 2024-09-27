import RPi.GPIO as GPIO
import time
import sqlite3
from datetime import datetime
GPIO.setwarnings(False)

SPEED_OF_SOUND = 34300      #Speed of sound in air (cm/s)
TANK_EMPTY = 8             #Height of tank 8cm in this case
HUNDRED_MILLISECONDS = 0.1  
ULTRASONIC_SENSOR_OFF = 0   #Echo_pin off
ULTRASONIC_SENSOR_ON = 1   #Echo_pin on


#Set board numbering
GPIO.setmode(GPIO.BOARD)

#Ultrasonicsensor Initialisation
trigger_pin = 19
echo_pin = 26

#Relay Initialisation
IN1 = 18
IN2 = 23
IN3 = 24

#Buzzer Initialisation
BuzzerIn = 5

#Ultrasonicsensor pin setup
GPIO.setup(trigger_pin, GPIO.OUT)
GPIO.setup(echo_pin, GPIO.IN)

#relay pin setup
GPIO.setup(IN1,GPIO.OUT)
GPIO.setup(IN2,GPIO.OUT)
GPIO.setup(IN3,GPIO.OUT)

GPIO.output(IN1,GPIO.LOW)
GPIO.output(IN2,GPIO.LOW)
GPIO.output(IN3,GPIO.LOW)

#Buzzer pin setup
GPIO.setup(BuzzerIn,GPIO.OUT)
GPIO.output(BuzzerIn,GPIO.LOW)

#Connect to the SQLite database
conn = sqlite3.connect('//home//Darlington//Capstone//Data//TFMSDB.db')
cursor = conn.cursor()

try:
    while True:
        #Trigger the ultrasonic sensor
        GPIO.output(trigger_pin, GPIO.LOW)
        time.sleep(HUNDRED_MILLISECONDS)
        GPIO.output(trigger_pin, GPIO.HIGH)

        #Measure the time it takes for the echo to return
        while GPIO.input(echo_pin) == ULTRASONIC_SENSOR_OFF:
            pulse_start = time.time()

        while GPIO.input(echo_pin) == ULTRASONIC_SENSOR_ON:
            pulse_end = time.time()

        pulse_duration = pulse_end - pulse_start

        #Calculate distance in cm
        Height = (pulse_duration * SPEED_OF_SOUND) / 2

        #Get current datetime
        Time = datetime.now().strftime('%Y-%m-%d %H:%M:%S')

        #Insert data into the database
        cursor.execute("INSERT INTO StorageTank(Height) VALUES (?,)",(Height,))
        conn.commit()
        #cursor = conn.cursor()

        #Variable to keep track of the last checked entry ID
        last_entry_id = 'Id'


        

        #Execute the query to check for new entry is empty tank
        cursor.execute("SELECT * FROM StorageTank WHERE Height = ? AND entry_id > ?", (TANK_EMPTY, last_entry_id))
        result = cursor.fetchone()

        if result:
            GPIO.output(IN1, GPIO.LOW)
        last_entry_id = result['Id']  #Update the last checked entry ID
        
        #Execute the query to check for new entry with height = 8
        cursor.execute("SELECT * FROM StorageTank WHERE Height = 8 AND entry_id > ?", (last_entry_id,))
        result = cursor.fetchone()
        if result:
        #If there is a new entry with height = 20, set the GPIO pin
            GPIO.output(IN2, GPIO.HIGH)
            last_entry_id = result['Id']  #Update the last checked entry ID
        time.sleep(2)
        
        #Variable to keep track of the last recorded height
        last_height = 0
        #Connect to the SQLite database
        #Check the state of GPIO pins 18 and 23
        if GPIO.output(IN1) == GPIO.HIGH and GPIO.output(IN2) == GPIO.HIGH:
            #Execute the query to fetch the latest height from the database
            cursor.execute("SELECT Height FROM StorageTank ORDER BY id DESC LIMIT 1")
            result = cursor.fetchone()

            if result:
                height = result[0]
                if height > last_height:
                    #If the height is increasing, set the GPIO pin LOW
                    GPIO.output(BuzzerIn, GPIO.LOW)
                    last_height = height
                else:
                    #If the height is not increasing, set the GPIO pin HIGH
                    GPIO.output(BuzzerIn, GPIO.HIGH)

        else:
            #If GPIO pins 18 and 23 are not both HIGH, set the GPIO pin HIGH
            GPIO.output(BuzzerIn, GPIO.HIGH)

        time.sleep(1)  #Delay for 1 second
except KeyboardInterrupt:
    GPIO.cleanup()
    conn.close()