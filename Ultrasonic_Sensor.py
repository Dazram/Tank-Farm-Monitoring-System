import serial
import sqlite3

# Open the arduino connection
ser = serial.Serial("//d


ev//ttyUSB0",9600)
# Connect to database












nnmhgmjuy;o'i
`




.
`
con = sqlite3.connect("//home//Darlington//Capstone//Data//TFMSDB.db", timeout=10)
# Create a cursor to execute SQL commands
cursor = con.cursor()

# Read and process data indefinitely
while True:
        if ser.in_waiting > 0:
           data = (int(ser.readline().decode().strip()))
           print("Received data:", data)  
           cursor.execute('INSERT INTO StorageTank(Height) VALUES(?)', (data,))
           con.commit()
        
           ser.close()
           con.close()
