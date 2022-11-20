from websocket import create_connection
import json
import struct

def prices():
    ws = create_connection("wss://stream.binance.com:9443/ws/Bitcoin")

    ws.send('{"method": "SUBSCRIBE","params": ["btcusdt@kline_1s"],"id": 1}')

    values = []
    
    for i in range(11):
        response = ws.recv()
        data = json.loads(response)
        inner = data.get("k")
        if inner != None:
            values.append(inner.get("o"))
            print(values)

    ba = bytearray()
    for j in values:
        num = float(j)
        obj = bytearray(struct.pack("f", num))
        obj = obj[::-1]
        ba += obj
    print(ba)         
    return ba
#prices()