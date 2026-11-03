# Collect and avoid

Unity Version: Unity 6.3 LTS (6000.3.19f1)
Render Pipeline: URP

Controls:
- WASD stick to move
- Shift/Space to perform a dash
- Esc to pause/unpause the game

3rd party assets used:
Coins pickup effect: https://pixabay.com/sound-effects/film-special-effects-coin-257878/
PrimeTween: 

Scripts:
- StateMahine.cs - a MonoBehaviour class that controls its current State and performs State transitions based on conditions.
- State.cs - an abstract class that has virtual methods that are called from StateMachine.
- GameBootstrapper.cs - a MB class that initializes the game (scene) (e.g. injecting common data into Enemies).
- GameManager.cs - controls the game state by reading events the player.
- PlayerCollisionDetector.cs - contains and triggers events about collision into enemies or collectibles.
- PlayerMovementController.cs - is responsible for player controls and movement.
- EnemyAI.cs - an MB that holds references to AI components and setting values. Initializes its state machine.
- EnemyState.cs - a base State class for enemy AI. Holds a protected reference to EnemyAI.
- PatrolState.cs - EnemyState that makes the enemy find a patrol point and move to it repeatedly.
- ChasinState.cs - EnemyState that makes the enemy chase the player or go to its last tracked position.
- Collectible.cs - a MB that simply rotates gameObjects's transform and has a public method for destroying it with playing a cool animation.
- CameraFollowController.cs - an MB that smoothly moves the camera's pivot to the player position.

What I learned and what was hard:
Learned that I need to subtract an origin position from the target one to get a direction.
Had a fun time implementing such an enemy AI first time. A lot of time was spent on RayCast setting, because I hadn't practiced RayCasting in a long time and didn't know how to calculate a vector direction to throw a RayCast.

Known bugs:
- Coin pickup sound plays with latency. It's not very noticeable though.
