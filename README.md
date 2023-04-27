# Side-scroller Task
 A task from Orizon Group to make a 2D sidescroller.

## Requirements

* The game should have a 2D side-scrolling view.
* The player should control a character that runs forward automatically.
* The player should be able to move the character left or right by moving the mouse.
* Obstacles should randomly appear on the screen and move towards the player.
* The player should avoid the obstacles by moving left or right.
* The player should score points for each second that they survive.
* The game should end when the player collides with an obstacle.

## Theme

Decided to go with a space launch vibe. The player is
a space shuttle and dodges asteroids.

## Technicalities

* score will be incremented only when a full second has passed
* there will be a somewhat constant stream of random asteroids
* any collision with an asteroid results in death
* asteroids has varying speeds
* there will be power ups, which will be collected for immediate
or later use:
  * shield, which disables collisions with asteroids or cracks them on collision
  * blast (name may be changed), which destroys all asteroids on screen
  * backwards thrusters, which slows down the movement of asteroids
* sfx
* vfx
* background music, arcade themed
* background view, looping sprite
* (maybe) varying asteroid sizes
* (maybe) varying asteroid patterns (a series of asteroids with pre determined
timing, count, position and size)

## Gameplay loop

The game waits for input before starting a count down (3 maybe 5 seconds).
After the count down finishes the game starts and slowly accelerates to top speed.
Asteroids start to come after a set amount of time, possible patterns
come a bit later, after the player gets comfortable with the controls and power ups.
After death the player can start the came again by providing any input or possibly
saving their score and then starting again.