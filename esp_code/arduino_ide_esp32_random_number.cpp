#include <WiFi.h>
#include <PubSubClient.h>
#include <Arduino.h>

// Dane do połączenia z WiFi
const char* ssid = "";
const char* password = "";

// Dane do połączenia z brokerem MQTT
const char* mqtt_server = "192.168.1.32";
const char* mqtt_user = "mqtt_user";  // Nazwa użytkownika MQTT
const char* mqtt_password = "mqtt_pass";  // Hasło użytkownika MQTT

// Unikalny identyfikator urządzenia, np. "ESP32-001"
const char* device_number = "ESP32-001";

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
  Serial.println("Connecting to WiFi...");
  WiFi.begin(ssid, password);
  int attempts = 0;
  while (WiFi.status() != WL_CONNECTED && attempts < 20) {
    delay(500);
    Serial.print(".");
    attempts++;
  }
  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("WiFi connected");
    Serial.print("IP address: ");
    Serial.println(WiFi.localIP());
  } else {
    Serial.println("Failed to connect to WiFi!");
  }
}

// Funkcja do ponownego łączenia się z brokerem MQTT
void reconnect() {
  while (!client.connected()) {
    Serial.print("Attempting MQTT connection...");
    // Próba połączenia z brokerem MQTT
    if (client.connect(device_id.c_str(), mqtt_user, mqtt_password)) {
      Serial.println("connected");
      // Subskrypcja na temat zapytań dla tego urządzenia
      client.subscribe((String("request/") + device_number).c_str());
      Serial.print("Subscribed to: ");
      Serial.println(String("request/") + device_number);
    } else {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Odczekanie przed ponowną próbą połączenia
      delay(5000);
    }
  }
}

// Funkcja wywoływana po otrzymaniu wiadomości na subskrybowany temat
void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Message arrived [");
  Serial.print(topic);
  Serial.print("] ");
  for (unsigned int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();

  if (String(topic).equals(String("request/") + device_number)) {
    // Losowanie liczby z zakresu 10-999
    int randomNumber = random(10, 100);

    // Komponowanie wiadomości do publikacji
    String message = "Device: " + String(device_number) + ", ID: " + device_id + ", Number: " + String(randomNumber);

    // Publikowanie odpowiedzi na temat odpowiedzi dla tego urządzenia
    client.publish((String("response/") + device_number).c_str(), message.c_str());
    Serial.print("Published: ");
    Serial.println(message);
  }
}

// Główna pętla programu
void loop() {
  if (!client.connected()) {
    reconnect(); // Ponowne łączenie się z brokerem MQTT, jeśli nie jesteśmy połączeni
  }
  client.loop(); // Obsługa klienta MQTT
}