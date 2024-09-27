import serial 
import sqlite3

#establish connection to MySQL. You'll have to change this for your database.
dbConn = sqlite3.connect("//home//Darlington//Capstone//Data//TFMSDB.db")
#open a cursor to the database
cursor = dbConn.cursor()

#this will have to be changed to the serial port you are using
device ='/dev/ttyUSB0' 
try:
  print("Trying...",device) 
finally:
  arduino = serial.Serial(device, 9600)  

try: 
  data = arduino.readline()  #read the data from the arduino
  #split the data by the tab
  #Here we are going to insert the data into the Database
  try:
    cursor.execute("INSERT INTO StorageTank (Height) VALUES (%s)", (data,))
    dbConn.commit() #commit the insert
    cursor.close()  #close the cursor
  except sqlite3.IntegrityError:
    print("failed to insert data")
  finally:
    cursor.close()  #close just incase it failed
except:
  print("Failed to get data from Arduino!")
