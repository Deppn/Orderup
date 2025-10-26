🍔 OrderUp – Restaurant Simulation Game (Unity)

OrderUp is a restaurant simulation game built with Unity, where you play as both a cashier and a waiter who must serve customers quickly and accurately.
Your goal is to prepare and deliver food orders to customers in the correct queue order to keep them happy and earn a high score.

🎮 Controls
🖱️ Scroll Wheel Forward → Move Forward
🖱️ Scroll Wheel Backward → Move Backward
🖱️ Left Click → Interact or Pick up items
🖱️ Right Click → Drop / Remove the item currently held

👥 Key Features
🧍‍♂️ Dynamic Queue System
Customers (NPCs) enter the restaurant, line up, place their orders, and leave once they receive the correct food.

🍱 Interactive Ordering System
Each NPC displays their order as text above their head. The player must prepare and deliver the matching food items.

🧠 Realistic NPC Behavior
NPCs follow a waypoint-based path:
Waypoint 0 → Spawn point
Waypoints 1–3 → Queue positions (3 = front of line)
Waypoint 4 → Cashier counter
Waypoint 5 → Exit (NPC is destroyed upon reaching this point)

🍔 Food Spawning System
Players can spawn food prefabs at specific positions (e.g., left or right of the camera) to prepare customer orders.

🕒 Random Spawn Timing
New NPCs appear every 10–20 seconds if a queue slot is available, keeping the gameplay flow dynamic and unpredictable.

🧩 Technologies Used
Unity 2019 / 2022
C# (NPC Queue System, Item Handling, Input Control)
Mixamo for NPC animations
Custom Prefab Spawner and Waypoint Navigation System

🚀 Download :
https://drive.google.com/file/d/14POzHmp1oWwDvVkJ6a4bq5PlUxVcXfKB/view?usp=sharing


🧠 Project Purpose
OrderUp was developed as an interactive simulation project to explore:
NPC queue and pathfinding logic
Player–object interaction using mouse input
Real-time object and prefab management in Unity

🖼️ Gameplay Preview
![til](./ezgif-156bcd08a6adc6.gif)

📜 License
This project is open-source for educational purposes.
You are free to use or modify it as long as you credit the original developer.
