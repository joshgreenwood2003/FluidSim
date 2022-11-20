import socket
from websocket2 import prices
from doorData import doors

# Create a client socket
clientSocket = socket.socket(socket.AF_INET, socket.SOCK_STREAM);

# Connect to the server
clientSocket.connect(("10.247.31.69",6970));

data = prices()
clientSocket.send(data) 

#data2 = doors()