# Baten Kaitos Origins Damage Calculator

This tool aims to simulate combos and calculate the damage output of each hit as accurately as possible.

[Download](https://github.com/Exchord/Baten-Kaitos-Origins-Damage-Calculator/releases)

Information on magnus, enemies, and combos: [BKO Documentation](https://docs.google.com/spreadsheets/d/1wXsL9PXnyIuvRiYNgX5p6uTaVBgJhXU1CDzXNFiwLRU/view#gid=1457790647)

![](https://i.imgur.com/Y8SHiK3.png)

The above screenshot is a simulation of [this combo](https://youtu.be/n9rcfXrhIZE?t=2503).

## User interface

### Character icons
![](https://i.imgur.com/ScIh0tO.png)  
Click any of the character icons to switch characters. The hand will then update to show only cards that the current character can use.


### Levels
![](https://i.imgur.com/o8kFhQ0.png)  
You can change a character's level by typing on the keyboard, using the mouse wheel, or clicking the dropdown arrow.  
If your quest magnus inventory causes your level to increase, the actual level will be displayed in green below the text box. If it decreases, it will be displayed in red.


### Auras
![](https://i.imgur.com/F6iwEv6.png)  
Auras purchased in the Endmost Bethel can be selected via these dropdown menus.  
Note that the strength of an aura may change depending on the character's level.


### Equipped magnus
![](https://i.imgur.com/za5gX3z.png)  
Sometimes you may already have a magnus equipped at the start of a combo. Right-click any equipment magnus in your hand, transparent or not, and it will appear next to the character's aura. You can then adjust the durability using the nearby combo box, or click the equipped magnus to remove it.


### Target
![](https://i.imgur.com/SBMVbvA.png)  
Click the target image to open the target selection window. When you select a new target, the main window will update with some of the enemy's data:
- Name
- Knockdown threshold
- Knockout threshold
- Final phase checkbox
- Shield checkbox
- HP
- Armor durability

![](https://i.imgur.com/EVosQWd.png)

### Knockdown and knockout

The enemy gets knocked down/out when their crush status reaches the knockdown/knockout threshold displayed here. Enemies that don't have a knockdown/knockout threshold cannot be knocked down/out.


### Final phase

Some bosses have multiple fights, the last of which is always different in at least one of the following aspects:
- Effective HP
- Shield
- Armor
- Multiplier
- The Godling's Rapture critical hits


### Shield

Some bosses have a shield that visibly and audibly breaks when their effective HP drops below 50%. When this happens, the following stats are permanently multiplied by 0.8:
- All defenses
- Crush limit
- Knockdown threshold
- Knockout threshold

The checkbox will toggle itself on or off depending on the value in the HP text box. If an enemy with a broken shield heals beyond the 50% threshold, the shield will not come back. In that situation, you can manually toggle the shield off.


### Secondary target

If this is checked, only hits from multi-target attacks will register.

Note that this program does not simulate multiple targets. If you use multi-target attacks, you may need to pre-equip your weapon and decrease its durability to compensate for hits on other targets.


### HP and effective HP
![](https://i.imgur.com/6aK0xCY.png)  
In the HP text box, you can enter a new value for the enemy's current HP, or use the mouse wheel to fine-tune the value. Next to the text box is the enemy's max HP, which you can click to reset the enemy's HP to the maximum.

Below HP, you may find something called "effective HP". This refers to boss battles that end with the enemy striking down the party or a cutscene interrupting the fight. Effective HP aims to replace true HP with the amount of damage required to end the battle.


### Down

If the enemy is knocked down, knocked out, or asleep, the next hit will be a critical hit.


### Status

You can select one of four status effects for the target: flames, frozen, shock, and blind. Attacking a burning enemy with ice, a frozen enemy with fire, or a blind enemy with light will cause a critical hit. Attacking a shocked enemy with any element will knock them down.

Note that these four status effects cannot be combined. However, sleep and poison can be combined with each other and with any of the other four effects.


### Armor durability

The following enemies have armor equipped at the start of a battle:  
Armored Cancerite, Armored Balloona, Armored Mite, Phoelix, Machina Arma: Razer 3 (final phase only)

After a certain number of hits, the armor will break. You can select how many hits are left using this dropdown menu.  
Note that Machina Arma: Razer 3's armor breaks along with its shield. Therefore, it doesn't have durability points.


### Deck
![](https://i.imgur.com/dQmxp1I.png)  
Click the Weak Attack magnus to open a deck editor. Here, you can select which cards will show up in each character's hand.  
Each row of magnus forms a group which you can enable or disable using the plus or minus button next to it. You can also remove a magnus from your deck by middle-clicking it in the main window.

![](https://i.imgur.com/RK4B8ku.png)

### Quest magnus
![](https://i.imgur.com/CdmQDLP.png)  
Click the blank magnus to open the quest magnus window. Here, you can add or remove any quest magnus to change your inventory. Some quest magnus add extra damage, allow for critical hits, or change all characters' levels. Magnus with no relevant effects are transparent.

Clicking the "Reset" button will turn all quest magnus into blank magnus. You can also right-click magnus in your inventory to move them around.

![](https://i.imgur.com/o3lwDWu.png)

### Temporary boost
![](https://i.imgur.com/65ylqfB.png)  
Some battle magnus increase or decrease a character's offense or defense for two turns. You can change the boost target by clicking any of your party members. Clicking any of the first 8 magnus will then add a bonus to their offense. The top text box displays the offense increase for the current turn, and the bottom text box displays the offense increase for the next turn.

Clicking "Next turn" will advance the character to the next turn so that the top value is overwritten by the bottom value, which is then reset to 0. Clicking "Reset" will reset both values to 0.  
You can use the mouse wheel to increase or decrease both boost values by 1.

Notes:  
- One offense turn is any turn from the action bar in the top left corner of the screen. Selecting or discarding magnus does not count as such.  
- One defense turn is a combo of any length performed on an enemy, regardless of whether or not it's a relay combo.  
- Enemies also have offense boosts for every element, but only physical offense can affect damage output on enemies. This comes into play when attacking with Firedrake Regalia or Aetherdrake Regalia equipped.

![](https://i.imgur.com/3eDx2t5.png)

### Dolphin
![](https://i.imgur.com/zRxfMiW.png)  
If the game is running in Dolphin 5.0 or 4.0.2 (stable versions only), clicking this icon will import the following data from the emulator:
- Levels
- Auras
- Quest magnus
- Target
- HP
- Temporary boost
- Combo

These are not read from Dolphin:
- Equipped magnus
- Down
- Status
- Armor durability


### Spreadsheet
![](https://i.imgur.com/YcdbLcT.png)  
Clicking this button will copy the entire output table to the clipboard. You can then paste it into the spreadsheet software of your choice. Note that the copied data is in plain text and contains cell references which only work if pasted at A1. After pasting it, you can move the table anywhere you want.

This feature allows you to experiment beyond the limitations of the calculator and simulate very specific scenarios, such as blindness misses and auras or quest magnus expiring mid-combo. This is also the only way to simulate hacked conditions or unexplored effects.


### Settings
![](https://i.imgur.com/bXoduDy.png)  
- <i>Auto-close target window:</i> When you choose a target, the target window closes automatically.

- <i>Highlight hits when hovering over an attack card:</i> When you hover over an attack card in your combo, all of its hits in the output table are highlighted in blue.

- <i>Read combo from Dolphin:</i> If this is checked, you can import combos from the game by clicking the Dolphin button.

- <i>Show effective HP remaining:</i> Switch between true HP and effective HP for the last row in the output table. You can also click the row title in the main window to toggle this setting.

- <i>Guillo's retroactive EX combo bonus:</i> If any of Guillo's standard attacks (except Medium Attack) directly precedes an EX combo, the projectile will likely hit the target after the EX combo bonus becomes active. This depends on the attack and the distance between Guillo and the enemy.

- <i>Secret Queen after enemy gets up:</i> Secret Queen and Secret Queen II are EX combos that can only be triggered if the enemy is down. If the enemy gets up as Milly runs toward them, the first hit will not be a critical hit.

- <i>Saber Dragon Horn:</i> Using this item raises the durability of equipped magnus to their max durability plus 5. If this setting is checked, the dropdown menus for equipped magnus will allow you to add up to 5 extra durability points.

- <i>Random hits:</i> Heavenlapse and Aphelion Dustwake are multi-target attacks that randomly hit or miss enemies. Here, you can select which of the 9 or 13 projectiles will hit the target.

- <i>Show tooltips</i>
	- Variables: If you hover over any of the row titles in the output table, a brief explanation will pop up.
	- Enemies, magnus (secondary windows): Shows the name of the enemy or magnus when hovering over it.

- <i>1st member, 2nd member:</i> Changes the order of party members in the main window and in Temporary Boost.


The checkboxes on the right side allow you to hide or show any of the rows in the output table. You can also hide a row by middle-clicking its title in the main window.

![](https://i.imgur.com/cwqVzgS.png)

### Combo results
![](https://i.imgur.com/DAs7OdF.png)  
In a similar fashion to the in-game text after a combo, this shows the number of cards and hits as well as the total damage and TP bonus of the current combo.


### Next combo

Clicking this button changes the status of your party and the target to what it will be after the current combo. This also clears the combo. You can then move on with the next combo without having to manually adjust the following input data:
- Equipment and durability
- HP
- Down (uncheck this if the enemy gets up before the next combo)
- Status
- Armor durability
- Temporary boost


### Hand
![](https://i.imgur.com/spssPNP.png)  
Here, you can see all the magnus from your deck which can be used by the currently selected character. Click any of these cards to add it to the combo. Cards will become transparent and unusable depending on their spirit number and that of the last card in your combo.

Right-click any equipment magnus in your hand, transparent or not, to have it equipped before the start of the combo.  
Middle-click any magnus to remove it from your deck.


### Combo
![](https://i.imgur.com/RbZO6EU.png)  
This is the string of cards that makes up your current combo. Click any card to remove it and all subsequent cards from the combo. Press Backspace to remove the last card from your combo.

You can also remove a single card anywhere in the combo by right-clicking it.


## Tips

You can use the mouse wheel to adjust levels, auras, durability, HP, status, boost, and random deviations.


## Output table


<i>Base offense:</i> An offense value based on the party member's level.

<i>Attack offense:</i> An offense value from an attack. Every hit within an attack has its own offense value.

<i>Attack crush:</i> A crush value from an attack. Every hit within an attack has its own crush value.

<i>Attack boost factor:</i> Some magnus grant a 2-turn offense and crush bonus. This factor equals 1 + boost.

<i>Offense deviation:</i> A random offense deviation ranging from -4% to +4%. Click the row title to reset the offense deviation on all hits.

<i>Crush deviation:</i> A random crush deviation ranging from -4% to +4%. Click the row title to reset the crush deviation on all hits.

<i>Electric Helm factor:</i> Bonus factor (1.2) from Electric Helm or Blitz Helm when using lightning attacks.

<i>Weapon offense:</i> Additional offense from equipped weapon.  
Click any cell in this row to enable a mid-combo status effect for specific weapons. Click the row title to remove all mid-combo status effects.

<i>Weapon defense:</i> Additional crush from equipped weapon.  
Click any cell in this row to enable a mid-combo defense/offense reduction for specific weapons. Click the row title to remove all mid-combo defense/offense reductions.

<i>Element compatibility:</i> Depending on the elements of the attack and equipped weapon, only a portion of the weapon bonus may take effect.

<i>Weapon factor:</i> A critical hit factor that increases the strength of weapons such as Excalibur or Dragonbuster.  
Cutthroat Knife has a 50% crit chance. When it is equipped, click any cell in this row to enable a critical hit. Click the row title to remove all Cutthroat Knife critical hits.

<i>Quest magnus bonus:</i> Offense and crush bonus from quest magnus.

<i>Aura offense:</i> Offense bonus from the party member's aura.

<i>Crush offense:</i> Crush bonus from the party member's aura.

<i>EX combo offense factor:</i> During an EX combo, a bonus factor is applied to all the above offense values.

<i>EX combo crush factor:</i> During an EX combo, a bonus factor is applied to all the above crush values.

<i>Critical hit factor:</i> Critical hits will apply a factor to all the above offense and crush values. Which factor is used depends on the enemy's status, the attack element, and whether or not the critical hit was caused by quest magnus (random).  
Click any cell in this row to enable a random quest magnus based critical hit. Click the row title to remove all random critical hits.

<i>Base defense:</i> Every enemy has six defense values - one for each element. After a shield break, all defenses are permanently multiplied by 0.8.

<i>Crush limit:</i> The resilience of the enemy's defense. When the enemy's crush status reaches this value, the total defense will be zero. After a shield break, the crush limit (as well as the knockdown and knockout thresholds) are permanently multiplied by 0.8.

<i>Crush status:</i> The crush status increases during a combo. The higher it gets, the lower the enemy's total defense will be on the next hit.  
The ratio of the crush status to the crush limit is used as a defense factor. For instance, if crush status is 60 and crush limit is 100, only 40% of the enemy's defense will be in effect on the next hit.  
When the combo ends, the crush status gets reset to 0 so that the full defense will be in effect when the next combo begins.

<i>Defense boost factor:</i> Some items or enemy moves can change the enemy's defense for two turns. This factor equals 1 + boost.

<i>Defense deviation:</i> A random defense deviation ranging from -4% to +4%. Click the row title to reset the defense deviation on all hits.

<i>Total offense:</i> total_offense = (base_offense * attack_offense * attack_boost_factor * random_offense_factor * electric_helm_factor + weapon_offense * element_compatibility * weapon_factor + quest_magnus_bonus + aura_offense) * ex_combo_offense_factor * critical_hit_factor

Total offense can be negative if attack_boost_factor is negative (offense boost < -1).

<i>Total crush:</i> total_crush = (base_offense * attack_crush * attack_boost_factor * random_crush_factor * electric_helm_factor + weapon_crush * element_compatibility * weapon_factor + quest_magnus_bonus + aura_crush) * ex_combo_crush_factor * critical_hit_factor

Total crush can be negative if attack_boost_factor is negative (offense boost < -1).

<i>Total defense:</i> total_defense = base_defense * (1 - crush_status / crush_limit) * defense_boost_factor * random_defense_factor

Total defense can be negative defense_boost_factor is negative (defense boost < -1).  
Total defense is 0 if crush_status >= crush_limit.

<i>Armor:</i> Additional defense from the enemy's equipped armor.

<i>Multiplier:</i> This factor is applied to the damage and crush output. It is 0.1 for machina armas before Sagi's awakening, and 0.2 when Guillo attacks a Sandfeeder.

<i>Damage output:</i>  
If total_offense > total_defense + armor_defense Then  
&emsp;damage_output = (total_offense - total_defense * 0.775 - armor_defense) * multiplier  
Else  
&emsp;damage_output = (total_offense - total_defense * 0.1 - armor_defense) * 0.25 * multiplier  
End If

Damage output is truncated and cannot be negative. If the result is less than 1, there is a 75% chance that damage output will be adjusted to 1, provided that armor_defense = 0 and multiplier = 1.  
Click any cell in this row to enable a "minimum 1" hit. Click the row title to disable all "minimum 1" hits.

<i>Crush output:</i>  
If total_crush > total_defense * 0.5 + armor_defense Then  
&emsp;crush_output = (total_crush - total_defense * 0.3875 - armor_defense) * multiplier  
Else  
&emsp;crush_output = (total_crush - total_defense * 0.05 - armor_defense) * 0.25 * multiplier  
End If

Crush output cannot be negative.

<i>Total damage output:</i> The total damage output so far.

<i>Total crush output:</i> The total crush output so far. Yellow cells indicate a knockdown at the end of the combo. Orange cells indicate a knockout at the end of the combo.

<i>(Effective) HP remaining:</i> The enemy's HP after each hit. Click the row title to toggle effective HP for "unbeatable" bosses. A shield break will be highlighted in pink.



## Advanced mechanics


### Regalia hits

If Milly has Firedrake Regalia or Aetherdrake Regalia equipped, an extra hit will be added to the end of her turn. The element of this extra hit is always physical.

total_offense = enemy_offense * 50 * enemy_physical_attack_boost_factor * random_offense_factor

Crush output is 0. The rest of the damage formula is the same as regular hits.


### Knockdown hits

If an enemy gets knocked down with fire (no flames immunity) or darkness at the end of a turn, an extra hit will be added to the combo.

total_offense = (knockdown_hit_offense + quest_magnus_bonus) * random_offense_factor  
max_damage_output = max_HP * 0.1 * random_factor  
damage_output = min(total_offense, max_damage_output)

Both random factors can range from 0.8 to 1.04. Damage output is truncated. Crush output is 0.  
knockdown_hit_offense is displayed as "Offense" in the game's menu. It follows these rules:  
- Sagi: knockdown_hit_offense = base_offense * 10  
- Milly: knockdown_hit_offense = base_offense * 7  
- Guillo: knockdown_hit_offense = base_offense * 15

knockdown_hit_offense is rounded to the nearest integer.