# StateMachineAI
WebGL build at https://jhocking.itch.io/state-machine-ai

This is a demo of a simple AI that uses a state machine and waypoints. Arrow keys move the player, while the enemy behavior is:<br>
* slowly patrol waypoint path until see player
* brief pause for guard noticing player, then give chase
* if the player goes around a corner just move to last place seen
* once reach that target check if can see player now
* if not then pause (maybe look around) before returning to patrol
* navmesh back to path, with obstacle avoidance at all times

resources for first pass:<br>
* 18:40 state machine https://www.youtube.com/watch?v=YdERlPfwUb0
* https://www.raywenderlich.com/16977649-pathfinding-with-navmesh-getting-started

later improvements:<br>
* more reliable path progress https://mobile.twitter.com/eriknordeus/status/1546807619247013889
* waypoint editor https://www.youtube.com/watch?v=MXCZ-n5VyJc
* improved behavior https://www.youtube.com/watch?v=6BrZryMz-ac
* multiple agents https://www.youtube.com/watch?v=qzkOslgzuzo
* sensor toolkit :30 https://www.youtube.com/watch?v=37z6hTHuJRI
* follow player breadcrumbs https://www.youtube.com/watch?v=cyvdAYOxnqg
