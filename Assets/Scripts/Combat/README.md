This folder contains combat-related components:

- Health: shared health for any actor (player, enemies). Has events and optional destroy-on-death.
- DealDamageOnCollision: applies fixed damage to a target tag upon collision/trigger.

Usage notes:
- For player, add `Health` and set `destroyOnDeath` to false if you prefer handling death in UI.
- For enemies, add `Health` and leave `destroyOnDeath` true so they despawn on death (spawner counts via OnDestroy).

