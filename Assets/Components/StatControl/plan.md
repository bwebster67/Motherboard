
### All stats able to be affected by components:

#### Player Stats
- Max HP
- Movespeed
- Regen

#### Weapon Stats
- Damage
- Cooldown
- Knockback
- Pierce
- Projectile Count
- Projectile Size
- Projectile Speed 

How do I achieve this?

1. When reload motherboard, loop through all passives first, keeping track of stat updates.
2. Then, call a WeaponComponentInstance function to reload all stats for that weapon, given the updates
3. Then, call a function to update the players stats, given the updates

How to keep track of updates?

1. Make Stat Enum
2. Make WeaponStatPayload class
3. Make PlayerStatPayload class 