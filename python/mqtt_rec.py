import paho.mqtt.client as mqtt
import time

# Adres IP brokera MQTT
mqtt_server = "" # [!] tutaj wpisać poprawne dane
mqtt_user = "mqtt_user"
mqtt_pass = "mqtt_pass"

# Zmienna do przechowywania czasu ostatniego ping dla każdego urządzenia
last_ping_time = {}
ping_interval = 10  # Oczekiwany odstęp czasu między pingami w sekundach

devices = ["esp32_c3_01", "esp32_c3_02"] #, "esp32_c3_03"]

# Funkcja wywoływana po połączeniu z brokerem MQTT
def on_connect(client, userdata, flags, rc):
    print(f"Connected with result code {rc}")
    for device in devices:
        client.subscribe(f"response/{device}")

# Funkcja wywoływana po otrzymaniu wiadomości
def on_message(client, userdata, msg):
    global last_ping_time
    device_id = msg.topic.split("/")[1]
    if device_id in devices:
        print(f"Message received from {device_id}: {msg.payload.decode()}")
        last_ping_time[device_id] = time.time()

# Konfiguracja klienta MQTT
client = mqtt.Client()
client.username_pw_set(mqtt_user, mqtt_pass)
client.on_connect = on_connect
client.on_message = on_message

client.connect(mqtt_server, 1883, 60)
client.loop_start()

def send_request():
    for device in devices:
        client.publish(f"request/{device}", "request_data")
        print(f"Request sent to {device}")

while True:
    current_time = time.time()
    for device in devices:
        if device in last_ping_time and (current_time - last_ping_time[device] > 2 * ping_interval):
            print(f"{device} not available")
        else:
            print(f"{device} is available")
    send_request()
    time.sleep(ping_interval)
