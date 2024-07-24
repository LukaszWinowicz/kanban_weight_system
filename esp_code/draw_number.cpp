#include <WiFi.h>
#include <PubSubClient.h>
#include <Arduino.h>

// Dane do połączenia z WiFi
const char* ssid = "";
const char* password = "";

// Dane do połączenia z brokerem MQTT
const char* mqtt_server = "";
const char* mqtt_user = "mqtt_user";  // Nazwa użytkownika MQTT
const char* mqtt_password = "mqtt_pass";  // Hasło użytkownika MQTT

// Unikalny identyfikator urządzenia, np. "esp32_c3_01"
const char* device_number = "esp32_c3_01";

// Uzyskanie unikalnego identyfikatora ESP32
String getUniqueID() {
  uint64_t chipid = ESP.getEfuseMac(); // The chip ID is essentially its MAC address
  char uniqueID[25];
  snprintf(uniqueID, 25, "ESP32_%04X%08X", (uint16_t)(chipid>>32), (uint32_t)chipid);
  return String(uniqueID);
}

String device_id;

WiFiClient espClient;
PubSubClient client(espClient);

void setup() {
  Serial.begin(115200);
  device_id = getUniqueID(); // Przypisanie unikalnego identyfikatora urządzenia
  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);
}

// Funkcja do łączenia się z WiFi
void setup_wifi() {
  delay(10);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("WiFi connected");
}

// Funkcja do ponownego łączenia się z brokerem MQTT
void reconnect() {
  while (!client.connected()) {
    // Próba połączenia z brokerem MQTT
    if (client.connect(device_id.c_str(), mqtt_user, mqtt_password)) {
      // Subskrypcja na temat zapytań dla tego urządzenia
      client.subscribe((String("request/") + device_number).c_str());
    } else {
      delay(5000); // Odczekanie przed ponowną próbą połączenia
    }
  }
}

// Funkcja wywoływana po otrzymaniu wiadomości na subskrybowany temat
void callback(char* topic, byte* payload, unsigned int length) {
  if (String(topic).equals(String("request/") + device_number)) {
    // Losowanie liczby z zakresu 0-9
    int randomNumber = random(0, 10);
    float temperature = 25.0 + random(0, 10) * 0.1; // Przykładowa losowa temperatura

    // Komponowanie wiadomości do publikacji
    String message = "Device: " + String(device_number) + ", ID: " + device_id + ", Number: " + String(randomNumber) + ", Temperature: " + String(temperature, 1);

    // Publikowanie odpowiedzi na temat odpowiedzi dla tego urządzenia
    client.publish((String("response/") + device_number).c_str(), message.c_str());
  }
}

// Główna pętla programu
void loop() {
  if (!client.connected()) {
    reconnect(); // Ponowne łączenie się z brokerem MQTT, jeśli nie jesteśmy połączeni
  }
  client.loop(); // Obsługa klienta MQTT
}