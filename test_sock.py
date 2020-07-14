import socket
import json

sock = socket.socket(socket.AF_INET)
sock.connect(("192.168.1.13", 6666))
sock.send(bytes(json.dumps(
    {"type": "", "from": "", "to": "", "howMany": 0}), 'utf8'))
sock.close()
