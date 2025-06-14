String receivedMessage = "";

void setup() {
  Serial.begin(9600);
  connectToPC(); //to connect to the pc
}

void loop() {
  String data = receiveFromPC(); 
  if (data != "") {
   
   //do somthing you could use select case for example 
   // lo for turning on led
   //loff for turing off led
   //led states to send led statues 
  }

  delay(100);

//Connect function (handshake)
void connectToPC() {
  delay(1000);
  Serial.println("Hello"); 
}

//  send message to PC
void sendToPC(String message) {
  Serial.println(message);
}

// Receive message from PC
String receiveFromPC() {
  String input = "";
  if (Serial.available()) {
    input = Serial.readStringUntil('\n');
    input.trim();
  }
  return input;
}
