

Player Control
 - You control a sunscreen bottle in this top down game
 - Where Arrows moves player, spacebar dash into sunscreen, and mouse to aim and spread sunscreen
 - Stats:
   - Hp
   - Speed
   - Damage
   - Defense
   - Crit

Basic Gameplay
 - Rougelike (Brotato)
 - During the game, enemies appear from outside the screen.
 - And the goal of the game is to defeat waves of enemies
 - Upgrade chance after [level up / defeating each wave]--(tbd: use level up for now)
 - Chest drop after defeating an mini boss/ boss
 - Single arena
 - Sunscreen spread around you when you shooting

Game flow:
state:
 - home
 - play

Chest:
 - Unlock Weapon
 - Drop coins
 - Upgrade weapon

Upgrade chance:
 - Hp
 - Speed
 - Damage
 - Defense
 - Crit
 - Extra Weapon
 - Peirce
 - Fire rate
 - (upgrade Charactor)

Free to play:
 - coin
   - drop every time player dies / get a chest
   - purchase skin
   - unlock charators
   - unlock drones
 - Ads:
   - 

Enemy Stats:
 - Hp
 - Speed
 - Damage

Enemy Type:
 - White eye: normal / Red eye(Elite): + HP and damage
 - Melee: average
 - Shooter: 
 - Sniper: cant move, low HP, aim with laser, delay shot, high damage
 - Tank: slow, high HP, damage between sniper and melee, slow attack speed
 - Assasin: fast, low HP, low damage, fast attack speed
 - Witch: spawn minions, downgraded Melee
 - Creeper: self-explode, speed: between melee and assasin, high AOE damage, rare

Boss:
 - Giant: (Elite Tank)
   - spawn one every 5 rounds
 - Commander:
   - 1st phase: throw Creeper (limited throwing distance)
   - 2nd phase: summon assasin around itself

Weapon:
 - Gun:
   - (default) Pistol: simple but reliable, average weapon stats, mid-high damage fall-off
   - Revolver: (reload every 8 shots) slow fire rate, mid-high damage, average damage fall-off
   - AR: (auto)mid fire rate, damage: pistol++, average+ damage fall-off
   - SMG: (auto)high fire rate, low damage, high damage fall-off, not accurate(spreading bullets)
   - Sniper Riffle: low fire rate, high damage, pierced fix number enemy (1+ by upgrade), no damage fall-off
   - Machine gun: (auto)high fire rate, delay shot, mid damage, overheat, slow movement

Weapon Upgrade:
   - (default) Pistol:
     - times 2
     - element bullet:
       - poinsonous
       - electric
       - freeze
       - fire
     - split bullets: sub-bullets spreading along the way
     - navigated bullets
   - Revolver:
     - Precise strike: damage increase with longer aiming time, reset after shot
     - Reload after roll/dash/dodge
     - Burst shot: shot all 8 bullet in a short time. less accurate, shorter range
   - AR: (auto)mid fire rate, damage: pistol++, average+ damage fall-off
     - bounce bullets:
     - mini missle: cool down, average damage,
     - sprint ability
   - SMG: (auto)high fire rate, low damage, high damage fall-off, not accurate(spreading bullets)
     - shoot when rolling: circle bullets
     - sheild: cool down
     - invisibility: first several shots increase crit
   - Sniper Riffle: low fire rate, high damage, pierced fix number enemy (1+ by upgrade), no damage fall-off
     - increase pierced enemy number
     - mark down: if enemy is out of camera, mark on edge of camera indicating enemy's direction
     - laser: infinite pierced, damage reduced
     - trap: trap enemies in a certain region, cannot move for several seconds
   - Machine gun: (auto)high fire rate, delay shot, mid damage, overheat, slow movement
     - damage reduction 20%
     - knockback
     - bigger bullets

Drone:
 - default: attack enemies
 - bomb:
 - revive HP:
 - extra life:
 - 

Sound & Effects
 - There will be sound effects and particle effect when player or enemies shoot, player or enemies got hit. 
 - There will also be Background Music

Gameplay Mechanics
 - As the game progresses, more waves of enemies appears, making it more difficult to survive

User Interface
 - The player's health will decrease whenever player got hit.
 - At the start of the game, no title will appear
 - Game will end when player has no health left

Project timeline
1. Weapons (non-upgraded) |Jason           100%
2. Enemy | Chris                            70%
3. Enemy | Chris                             0%
3. Drone |Jason                             90%
4. Spawn algorithm |Chris                    0%
6. Player level up systems | Jason           0%
7. Chest | Jason                             0%
8. healthPack | Jason                        0%
6. Upgrade weapons |Jason                    0%
7. Shop (coin system) |Chris                 0%
8. Menu |Jason                               0%
5. Level system (UI included) |Chris         0%
9. Ads |Chris                                0%