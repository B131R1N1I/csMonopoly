import socket
import json

sock = socket.socket(socket.AF_INET)
sock.connect(("192.168.1.13", 6666))
sock.send(bytes(json.dumps(
    {"type": "newPlayer", "from": "aaa", "to": "bbb", "howMany": 0.01}), 'utf8'))
sock.close()
