#include <Arduino.h>
#include "HX711.h"  // Dodaj bibliotekę HX711
#include <WiFi.h>  // Dodaj bibliotekę WiFi
#include <PubSubClient.h>  // Dodaj bibliotekę MQTT

// Definicja pinów dla HX711
const int LOADCELL_DOUT_PIN = 18;  // Pin DOUT podłączony do HX711
const int LOADCELL_SCK_PIN = 19;   // Pin SCK podłączony do HX711

// Inicjalizacja HX711
HX711 scale;

// Współczynnik kalibracji (początkowa wartość)
float calibration_factor = 400; 

// Dane do połączenia z WiFi
const char* ssid = "";
const char* password = "";

// Dane do połączenia z brokerem MQTT
const char* mqtt_server = "192.168.1.32";
const char* mqtt_user = "mqtt_user";  // Nazwa użytkownika MQTT
const char* mqtt_password = "mqtt_pass";  // Hasło użytkownika MQTT

// Unikalny identyfikator urządzenia
const char* device_number = "ESP32-001";

WiFiClient espClient;
PubSubClient client(espClient);

void setup_wifi() {
  delay(10);
  Serial.println("Łączenie z WiFi...");
  WiFi.begin(ssid, password);
  int attempts = 0;
  while (WiFi.status() != WL_CONNECTED && attempts < 20) {
    delay(500);
    Serial.print(".");
    attempts++;
  }
  if (WiFi.status() == WL_CONNECTED) {
    Serial.println("Połączono z WiFi");
    Serial.print("Adres IP: ");
    Serial.println(WiFi.localIP());
  } else {
    Serial.println("Nie udało się połączyć z WiFi!");
  }
}

void reconnect() {
  while (!client.connected()) {
    Serial.print("Próba połączenia z brokerem MQTT...");
    if (client.connect(device_number, mqtt_user, mqtt_password)) {
      Serial.println("połączono");
      client.subscribe((String("request/") + device_number).c_str());
      Serial.print("Subskrybowano: ");
      Serial.println(String("request/") + device_number);
    } else {
      Serial.print("błąd, rc=");
      Serial.print(client.state());
      Serial.println(" próba ponowna za 5 sekund");
      delay(5000);
    }
  }
}

void callback(char* topic, byte* payload, unsigned int length) {
  Serial.print("Wiadomość odebrana [");
  Serial.print(topic);
  Serial.print("] ");
  for (unsigned int i = 0; i < length; i++) {
    Serial.print((char)payload[i]);
  }
  Serial.println();

  if (String(topic).equals(String("request/") + device_number)) {
    // Odczytanie wartości wagi
    scale.set_scale(calibration_factor);  // Ustawienie współczynnika kalibracji
    float weight = scale.get_units(10);  // Pobranie uśrednionych 10 odczytów

    // Upewnienie się, że odpowiedź jest w poprawnym formacie liczbowym
    String weightStr = String(weight, 2);  // Zawsze dwa miejsca po przecinku
    String message = "Urządzenie: " + String(device_number) + ", Waga: " + weightStr + " gram";

    // Publikowanie odpowiedzi na temat odpowiedzi dla tego urządzenia
    client.publish((String("response/") + device_number).c_str(), message.c_str());
    Serial.print("Opublikowano: ");
    Serial.println(message);
  }
}

void setup() {
  Serial.begin(115200);
  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);

  // Inicjalizacja HX711
  scale.begin(LOADCELL_DOUT_PIN, LOADCELL_SCK_PIN);  

  Serial.println("HX711 Kalibracja");

  // Ustawienie wartości zerowej (tare)
  Serial.println("Ustawienie wartości zerowej...");
  scale.set_scale();  // Ustawienie skali na domyślną wartość (1)
  scale.tare();  // Zerowanie wagi
  Serial.println("Waga wyzerowana.");
}

void loop() {
  if (!client.connected()) {
    reconnect(); // Ponowne łączenie się z brokerem MQTT, jeśli nie jesteśmy połączeni
  }
  client.loop(); // Obsługa klienta MQTT
}
