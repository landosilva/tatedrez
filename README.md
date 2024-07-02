# ♟️♟️♟️ **TaTeDrez** ♟️♟️♟️

First and foremost, thank you Homa for this opportunity. It was a quite fun challenge and I actually had a lot of fun both developing and playing the result with my friends.
I apologise for the little delay in delivering it but please evaluate my submission as I would love to hear back from you.


https://i.diawi.com/rQkF4N

## 🎨 Art Choices
### 💡 Inspiration
Every time I start a new project I like to explore it visually first. I would say it has a lot of benefits, such as:
* Inspiration;
* Stimulation of creativity;
* Warming up my brain cells;
* Putting things in place gives me a clear vision of what I need to develop, thus being more productive in the long run.

That said, I went to the play store in search for chess games with interesting art styles and I found this one that I liked very much:

--image

### 🌅 Chosen Assets
Even though I didn't go trough with 3D environment, seeing and playing this game made the kick off for me and I went in the search of free art assets in places that I'm already familiar with.
I ended up using this 2D pixel art by ---, which can be found through this link: ---

After that, I searched for some simple UI and Misc elements that I could benefit from I liked this set by --- which can be found through this link: ---

Having all these setup, I did some little adjustments on the images to fit our needs and ended up with this scene:

--image

With that in place, I felt inspired and could start doing some actual code.

---

## 🧑‍💻 Technical Choices

### 📱 Player & Input
The very first thing that I did was deciding how would the player interact with the pieces and I went with the last Unity's Input System.

### ♟️ Piece Movement & Placement
Lorem Ipsum

#### Strategy Pattern
Lorem Ipsum

#### Animator State Machine Exploration
I was about to develop my own simple State Machine module for this test as well but then I remembered about StateMachineBehaviour and decided to play with it and use Animator Controller and Animation States to function as State Machine and States, respectively. Well, in the end, they are not call like it, but they are exactly that.
In other for that to work, I did GameState inheriting from it with the only job of storing what I called Blackboard, a very flexible place to store information about the object running the State Machine. It can be either by Key or, if not specified, the Key will be the stored object's type and will function similar to MonoBehaviour's GetComponent.
I have to say that the results were not bad, but next time I would go with a proper developed FSM.

#### Win Condition
Lorem Ipsum

### 🧃 Juice
Lorem Ipsum

#### Particle VFX & DOTween
Lorem Ipsum

#### Sounds
Victor is awesome

---

## ⚙️ Modules
As I mentioned, I didn't use any external tool except of DOTween. But I do did some reusable code and implementation of modules that could be exported as a package and used in any other project.

### ⚙️ Singletons
I know, I know. Singletons are not the cool kids in the park and a more robust solution would be Service Locators or Dependency Injection, but I do like to use them, specially for prototyping and simple projects like this. They are just, as any other solution or design pattern, a tool. Every tool can be misused, but also, every tool was created to solve a problem. And I believe Singletons are injusticied.

#### Mono Behaviour
I did a very simple implementation of a MonoBehaviour Singleton, which can be persistent across scenes or not.

#### Scriptable Objects
I also did a version for Scriptable Objects and actually I do believe having it is a very good way to store some types of data.

### ⚙️ Events
Just a basic and standard Event Bus pattern.

### ⚙️ Generators
#### Layers and Layer Masks
Lorem Ipsum

### ⚙️ Debugger
Lorem Ipsum

### ⚙️ Sound Database
Lorem Ipsum

---

## 🎖️ Conclusion
Lorem Ipsum