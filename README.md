# First-Year-Scholars-AR

Developed by KSU's first-year scholars 2025-2026 "Taking Augmented Reality Beyond the Lab" to navigates inside the Atrium building using Unity's augmented reality system. 

## Getting Started

### 1. Unity Prerequisites

- Install Unity version 6000.0.61f or later
- Switch to android builds

### 2. Import The Project

- Navigate to Window -> Package Manager
- Click the "+" icon at the top left -> Install package from git URL
- Copy & paste this git url:
```
https://github.com/KSU-FYS2025/First-Year-Scholars-AR.git
```

### 3. Setup The Project

- Search for the scene "Atrium Navigation" and open it. (Assets/Samples/MultiSet Quest SDK/1.9.2/Sample Scenes/Naigation/Atrium Navigation.unity) 

 
- [OPTIONAL] If you have your own map/mapsets then complete the following:
- [OPTIONAL] Find "MultisetSdkManager" in the scene and open the MultiSet config
- [OPTIONAL] If not already, create a MultiSet account in the developer portal: [https://developer.multiset.ai/auth/sign-up](https://developer.multiset.ai/auth/sign-up)
- [OPTIONAL] Paste the client ID and client secret into the Unity config and verify credentials
- [OPTIONAL] Paste your map or mapset code into "LocalizationManager"

- Find "MapMeshDownloader" -> press download mesh and wait for the scene to complete downloading the meshes
- If integrating an LLM into the STT system, make sure to enter the backend URL in the "RealTimeQuery" object.
  
### 4. How It Works

- Using the meta quest controller, press the "Open list button" (B by default) to access the points of interest in the Atrium building
- Search for a destination of your choice and simply press it to generate an arrow guiding to your destination and an approximate distance
- To navigate using speech-to-text: press the microphone button located at the top of the navigation list UI and follow the on-screen instructions
