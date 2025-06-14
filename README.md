# 🔌 Arduino - VB.NET Serial Communication Interface

This project demonstrates how to build a robust two-way communication system between an **Arduino** and a **VB.NET Windows Forms application** using **serial communication** over USB.

The system enables sending and receiving command strings — like turning on LEDs, requesting sensor data, or displaying statuses — in real-time, using clean and extendable logic.

> 🧠 This method is also used in my larger project: [Smart-Lighting-System-Manager](https://github.com/M7MEDpro/Smart-Lighting-System-Manger)

---

## 📁 Project Files

- `ArduinoManager.vb` – VB.NET class that handles:
  - Auto-connecting to Arduino through available COM ports
  - Sending commands to Arduino
  - Reading replies from Arduino
  - Triggering events when connection status changes

- `arduino_code.ino` – Arduino sketch that:
  - Responds to VB commands with LED control, sensor data, etc.
  - Sends data back to PC (sensor readings, acknowledgments)
  - Includes a handshake system using `"Hello"` for initial validation

---

## 🧠 How It Works

### 🔄 Connection Process

When the VB.NET app starts, it automatically scans all available COM ports, tries to send a `"Hello"` message, and expects `"Hello"` back. This validates that an Arduino is connected and listening.

Once confirmed, the port is considered active and two-way communication begins.

---

### 📤 Sending Commands from VB.NET

The `SendToArduino(String)` method allows the VB app to send commands like:

- `"ledon"` — Turns on LED
- `"ledoff"` — Turns off LED
- `"getldr"` — Requests current LDR value
- `"buzzeron"` — Turns buzzer on (if connected)
- `"temp?"` — Requests temperature from a DHT11 sensor (if used)

The commands are simple text strings that the Arduino interprets using `if` or `switch-case` logic.

---

### 📥 Receiving Data in VB.NET

The method `ReadFromArduino()` is used to read strings sent by the Arduino. These can include:

- `"led:on"` — Confirmation that the LED is on
- `"ldr:689"` — A light sensor value
- `"temp:25.3"` — A temperature reading
- `"error"` — Feedback in case something fails

This allows real-time updates on the VB.NET GUI.

---

## 💡 Examples of Use

| 🖥 PC Command  | 🔁 Arduino Action                    | ↩️ Arduino Response     | 🧩 VB.NET Reaction                   |
|---------------|--------------------------------------|--------------------------|-------------------------------------|
| `ledon`       | Turns on built-in LED                | `led:on`                 | Show “LED is ON” on GUI             |
| `ledoff`      | Turns off LED                        | `led:off`                | Show “LED is OFF” on GUI            |
| `getldr`      | Reads LDR (connected to A0)          | `ldr:712`                | Update LDR reading display          |
| `buzzeron`    | Activates buzzer                     | `buzzer:on`              | Indicate buzzer is active           |
| `temp?`       | Reads temperature sensor             | `temp:26.1`              | Display live temperature            |

---

## 🧱 VB.NET - Key Methods Summary

| Method                      | What It Does                                                            |
|-----------------------------|-------------------------------------------------------------------------|
| `Connect()`                 | Auto-connects to Arduino via available COM ports                        |
| `Disconnect()`              | Closes the serial port                                                  |
| `SendToArduino(string)`    | Sends a command (like "ledon") to Arduino                               |
| `ReadFromArduino()`        | Reads a line from Arduino                                               |
| `ConnectionStatusChanged`  | An event to track connection, error, or disconnection states            |

---

## 🤖 Arduino - Key Logic

| Function/Concept       | Purpose                                                             |
|------------------------|---------------------------------------------------------------------|
| `connectToPC()`        | Sends `"Hello"` during setup for handshake with PC                 |
| `receiveFromPC()`      | Reads incoming text commands from PC                               |
| `sendToPC(String)`     | Sends feedback or sensor values back to the PC                     |
| `loop()`               | Continuously waits for and reacts to messages from PC              |
| `if/switch-case`       | Executes the appropriate action based on the received string       |

---

## 🧩 GUI Integration (VB.NET)

- Use a Timer or BackgroundWorker to regularly call `ReadFromArduino()` and update labels/textboxes
- Use buttons to send predefined commands via `SendToArduino("...")`
- Display sensor feedback and LED/buzzer states in real-time
- Handle connection status via `ConnectionStatusChanged` event

---

## 🧠 Real Project Examples

### 1. ✅ Smart Lighting System Manager  
**Link:** [Smart-Lighting-System-Manager](https://github.com/M7MEDpro/Smart-Lighting-System-Manger)  
**Use Case:**
- Arduino uses LDR and Ultrasonic sensor to control lights based on ambient light and motion
- VB.NET GUI overrides automation, manually turns lights ON/OFF
- Communication: `"ldrhigh"`, `"ldrlow"`, `"motion:true"`, `"light:off"`

---

### 2. ✅ Soil Moisture Monitoring & Pump Controller  
**Use Case:**
- Arduino reads a capacitive soil moisture sensor and controls a water pump
- VB.NET sends command `"pump:on"` or `"pump:off"` for manual override
- Arduino sends feedback like `"soil:dry"` or `"soil:wet"`
- VB.NET GUI shows live moisture % and pump status

---

### 3. ✅ Home Security System Interface  
**Use Case:**
- Arduino monitors door sensors, gas sensors, and PIR motion detectors
- VB.NET GUI displays real-time alerts, and sends `"alarm:on"` to trigger siren
- Arduino replies with alerts: `"door:open"`, `"gas:detected"`, `"motion:yes"`
- PC can also log and timestamp these alerts

---

## 🛠 Requirements

- Arduino UNO or similar board
- VB.NET WinForms app (Visual Studio 2022+)
- USB cable for Serial communication
- Drivers installed for Arduino
- `System.IO.Ports` namespace enabled in VB.NET

---

## 📌 Tips for Smooth Usage

- Make sure Arduino IDE's Serial Monitor is closed before running VB.NET app
- Always match baud rate (default: 9600) on both ends
- Use `Try/Catch` when calling `ReadLine()` to avoid runtime crashes
- Add delay in Arduino after `Serial.begin()` before handshake
- Use structured string replies (e.g., `ldr:728`) for easy parsing

---

## 📜 License

Licensed under the MIT License – free to use, modify, and share.

---

## 🤝 Contributing

- Found a bug? Suggest an issue!
- Want to add new sensors or commands? Fork and submit a pull request.
- Feedback is welcome via GitHub issues or discussions.

---

## 🔗 Related Projects

- [Smart-Lighting-System-Manger](https://github.com/M7MEDpro/Smart-Lighting-System-Manger)
