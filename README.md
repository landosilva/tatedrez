# **TaTeDrez**

## 👁️ Preview
<img align="right" src="https://github.com/landosilva/tatedrez/assets/9856112/32988890-5e1c-4276-8d75-baa7b1c28fc9" alt="Preview" width="39%"/>
First and foremost, thank you, Homa, for this opportunity. It was quite a fun challenge, and I really enjoyed both developing and playing the game with my friends. I apologize for the slight delay in delivering it, but please evaluate my submission as I would love to hear your feedback.

I also took the freedom to add one more thing that I missed while I was playing with friends: timer for each player, as we have in competitive chess.

You can easily check it out here:

<img src="https://cdn-icons-png.flaticon.com/512/174/174836.png" alt="Android" width="16"/> **Android:**\
https://i.diawi.com/Xi2oyf \
<img src="https://www.diawi.com/qrcode/link/Xi2oyf" alt="rQkF4N" width="192"/>

You can navigate through my implementations choices below.

---

<details>
<summary>🎨 Art Choices</summary>

## 🎨 Art Choices  
### 💡 Inspiration
Every time I start a new project I like to explore it visually first. I would say it has a lot of benefits, such as:
* Sparking inspiration;
* Stimulation of creativity;
* Warming up my brain cells;
* Providing a clear vision of what I need to develop, making me more productive in the long run.

With this in mind, I searched the Play Store for chess games with interesting art styles and found one that I liked very much:\

<img src="https://static-00.iconduck.com/assets.00/google-play-icon-2048x2048-487quz63.png" alt="PlayStore" width="16"/> [**Pocket Chess**](https://play.google.com/store/apps/details?id=com.dkxqzbfkjt.pocketchess) \
<img src="https://play-lh.googleusercontent.com/iyLry93eL21lpxrRvyHx9XvHe2rFi7Ogobgqjsek1_VjDPBG4M13zKp2F_3alTRa3Rk=w5120-h2880-rw" alt="Pocket Chess 1" width="200"/>
<img src="https://play-lh.googleusercontent.com/t59kCZCFPPlGGdDtY4UdqA6bf6EUph4oenQVCDOaBmXWkAgm7B8Bq5o50gHgo8pNusw=w5120-h2880-rw" alt="Pocket Chess 2" width="200"/>
<img src="https://play-lh.googleusercontent.com/PgwMJugAXMOxv8n7TQarc0NvsMHfxMuajMKGKn9196XAaa9xiBLLdwgIHg2P9U0N90o=w5120-h2880-rw" alt="Pocket Chess 3" width="200"/>


### 🌅 Chosen Assets
Although I didn't go with a 3D environment, playing this game gave me the initial push to search for free art assets from familiar sources. I ended up using 2D pixel art by [dani-maccari](https://dani-maccari.itch.io/), which can be found [here](https://dani-maccari.itch.io/pixel-chess).\
<img src="https://img.itch.zone/aW1hZ2UvMTM0MDA5NC85NDUyMjkxLnBuZw==/794x1000/awyDGw.png" alt="Pixel Chess 1" width="200"/>
<img src="https://img.itch.zone/aW1hZ2UvMTM0MDA5NC85NDUyMjkwLnBuZw==/original/pyTgkh.png" alt="Pixel Chess 2" width="200"/>
<img src="https://img.itch.zone/aW1hZ2UvMTM0MDA5NC8xMjc5MTIyMS5wbmc=/original/nlTm2Q.png" alt="Pixel Chess 3" width="200"/>

Next, I searched for some simple UI and miscellaneous elements and found a set by [bdragon1727](https://bdragon1727.itch.io/), which can be found [here](https://bdragon1727.itch.io/basic-pixel-health-bar-and-scroll-bar).\
<img src="https://img.itch.zone/aW1hZ2UvMjA3MjY0NC8xMjE4OTg4My5naWY=/794x1000/hzg46C.gif" alt="Pixel UI" width="200"/>

With these assets, I made some minor adjustments to fit our needs and created this scene:\
<img width="200" alt="image" src="https://github.com/landosilva/tatedrez/assets/9856112/52c8254d-0df9-4074-ae97-2265e2e47d0c">


With that in place, I felt inspired and could start coding.
</details>

---

<details>
<summary>🧑‍💻 Technical Choices</summary>

## 🧑‍💻 Technical Choices

### 📱 Player & Input
The first thing I did was decide how the player would interact with the pieces, so I went with Unity's latest Input System.

### ♟️ Piece Movement & Placement
The player grab the piece by using Unity's Raycasts, and the placement is made doing several unit conversion calculations.

```chsarp
private void IndexToWorld(Vector2Int index, out Vector3 result)
{
    Vector2 unitOffset = _offset.ToUnits();
    result = index.Add(unitOffset);
}

private void WorldToIndex(Vector3 worldPosition, out Vector2Int result, bool clamp = true)
{
    Vector2Int inPixels = worldPosition.ToPixels() - _offset;
    result = inPixels.Divide(Constants.PixelsPerUnit);
    
    if (clamp)
        result.Clamp(min: Vector2Int.zero, max: _size - Vector2Int.one);
}

private void WorldToNode(Vector3 worldPosition, out Node result, bool clamp = true)
{
    WorldToIndex(worldPosition, out Vector2Int index, clamp);
    _map.TryGetValue(index, out result);
}
```

#### Movement Scriptable Object
To manage the different types of pieces and their movements, I decided to create a Scriptable Object with a Custom Editor in order to easily handle new movement types. It is called "Strategy" because I'm using the exact same system for win conditions, as I will show later.
<img width="400" alt="Movement Scriptable Object" src="https://github.com/landosilva/tatedrez/assets/9856112/8a5d12d2-6bc6-4745-9f34-ec179aa4fdb1">

#### Animator State Machine Exploration
I considered developing my own simple State Machine module for this test, but then I remembered `StateMachineBehaviour` and decided to use it along with Animator Controller and Animation States as a State Machine and States, respectively. Though they are not called that, they function exactly that way.

For this to work, I created a `GameState` inheriting from `StateMachineBehaviour` with the sole job of storing what I called the `Blackboard`, a flexible place to store information about the object running the State Machine. It can store information by key or by the object's type, functioning similarly to MonoBehaviour's `GetComponent<T>`.

```csharp
// By Type

_blackboard.Set(_board);
Board board = _blackboard.Get<Board>();

// By Key
_blackboard.Set(GameManager.Variables.Player.Current, nextPlayer);
PlayerSpot winner = _blackboard.Get<PlayerSpot>(key: GameManager.Variables.Player.Current);
```

<img width="640" alt="image" src="https://github.com/landosilva/tatedrez/assets/9856112/1c4bc93f-e46c-424d-aaa0-45099f0f739a">

The results were decent, but next time I would use a properly developed FSM.

#### Win Condition
As I mention, it's the exact same system used for movement, but to check specific board positions.
<img width="400" alt="Movement Scriptable Object" src="https://github.com/landosilva/tatedrez/assets/9856112/875c895b-e03d-4918-9bf6-03f629b6a890">

### 🧃 Juice
To enhance the game's feel, I tried to focus a lot on player's feedback with UI elements, animations and particle effects. 

#### Sounds
I added a custom background music, various sound effects for piece movements and win/lose conditions. A big shoutout to my personal friend [Victor Silva](https://settingscon.com/) for his awesome sound design work!

</details>

---

<details>
<summary>⚙️ Modules</summary>
  
## ⚙️ Modules
As mentioned, I didn't use any external tools except for DOTween. However, I did implement some reusable code and modules that could be exported as a package and used in other projects.

### ⚙️ Singletons
I know, I know. Singletons are not the cool kids in the park and a more robust solution would be Service Locators or Dependency Injection, but I do like to use them, specially for prototyping and simple projects like this. They are just, as any other solution or design pattern, a tool. Every tool can be misused, but also, every tool was created to solve a problem.

#### Mono Behaviour
I implemented a simple MonoBehaviour Singleton, which can be persistent across scenes or not.

#### Scriptable Objects
I also created a version for Scriptable Objects, which I believe is a great way to store certain types of data. I will show an example in the Sound section.

### ⚙️ Events
I used a basic and standard Event Bus pattern to handle game events efficiently.

```csharp
private static void NotifyStarted()
{ 
    Events.Started onStarted = new();
    Event.Raise(onStarted);
}

Event.Subscribe<GameManager.Events.Started>(OnGameStarted);
Event.Unsubscribe<GameManager.Events.Started>(OnGameStarted);
```

### ⚙️ Generators
#### Layers and Layer Masks
When I'm writing my code I usually like to first simply write as I would like to use it. With that in mind, eventually I came up with this little handy tool to convert Layer to an static class that also already converts to mask, and you can use like this:
```charp
int overlapped = Physics2D.OverlapCircleNonAlloc(position, radius: 0.1f, _buffer, Layer.Mask.Piece);
```

### ⚙️ Debugger
Just replacing `Debug.Log` by `Debbuger.Log` you will have all your logs stored and you can easily disable/enable them.
<img width="402" alt="Debbuger" src="https://github.com/landosilva/tatedrez/assets/9856112/6a70311e-e5dc-4a1a-875b-80af52a09677">


### ⚙️ Sound Database
This is making use of the Scriptable Object Singleton, and also in the same note as the Layer Generator, I did something similar for the sounds of the game. You can structure your Sound Databse as you please and generate a static class that can be used like this: 

```csharp
SoundManager.PlaySFX(SoundDatabase.Piece.Hold);
```

<img width="402" alt="image" src="https://github.com/landosilva/tatedrez/assets/9856112/0a3c5e69-a5e1-4585-9121-7382fe3b703d">


</details>

---

<details>
<summary>🎖️ Conclusion</summary>

## 🎖️ Conclusion
Overall, developing this challenge was a great experience where I took the chance to explore a few things that I was already interested in and I have to say that I'm proud of the final result and looking forward to hear from you.
Thank you very much!

</details>
