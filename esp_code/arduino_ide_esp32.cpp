#include <WiFi.h>
#include <PubSubClient.h>
#include <Arduino.h>

// Dane do połączenia z WiFi
const char* ssid = "your_SSID";
const char* password = "your_PASSWORD";

// Dane do połączenia z brokerem MQTT
const char* mqtt_server = "broker_IP_address";
const char* mqtt_user = "mqtt_user";  // Jeśli używasz uwierzytelniania
const char* mqtt_password = "mqtt_password";  // Jeśli używasz uwierzytelniania
const char* device_id = "esp32_c3_01";  // Unikalny identyfikator urządzenia

WiFiClient espClient;
PubSubClient client(espClient);

void setup() {
  Serial.begin(115200);
  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);
}

void setup_wifi() {
  delay(10);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connected");
}

void reconnect() {
  while (!client.connected()) {
    if (client.connect(device_id, mqtt_user, mqtt_password)) {
      client.subscribe((String("request/") + device_id).c_str());
    } else {
      delay(5000);
    }
  }
}

void callback(char* topic, byte* payload, unsigned int length) {
  if (String(topic).equals(String("request/") + device_id)) {
    int randomNumber = random(0, 10);  // Losowanie liczby z zakresu 0-9
    String message = String(randomNumber);
    client.publish((String("response/") + device_id).c_str(), message.c_str());
  }
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
}
