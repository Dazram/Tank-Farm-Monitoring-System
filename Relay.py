# Import RPi.GPIO library 
import RPi.GPIO as GPIO
import time

# Use BCM GPIO references 
# instead of physical pin numbers
GPIO.setmode(GPIO.BOARD)  

# Set trigger and echo pin numbers
TRIGGER_PIN = 17
ECHO_PIN = 13

# Set pins as output and input
GPIO.setup(17, GPIO.OUT)  
GPIO.setup(ECHO_PIN, GPIO.IN)

while True:

  # Trigger a pulse
  GPIO.output(TRIGGER_PIN, True)
  time.sleep(0.00001)
  GPIO.output(TRIGGER_PIN, False)

  # Start timer
  start = time.time()

  # Wait for echo pulse to stop
  while GPIO.input(ECHO_PIN)==0:
    pass
  start = time.time()

  # Stop timer when echo detected
  while GPIO.input(ECHO_PIN)==1:
    pass
  stop = time.time()

  # Calculate pulse length
  duration = stop - start

  # Distance in cm
  distance = duration * 17150
  distance = round(distance, 2)

  # Output distance  
  print("Distance (cm): ", distance)

  time.sleep(1)

# When done, reset GPIO settings  
  GPIO.cleanup()
