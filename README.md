# Baten Kaitos Origins Damage Calculator

This tool aims to simulate combos and calculate the damage output of each hit as accurately as possible.

[Download](https://github.com/Exchord/Baten-Kaitos-Origins-Damage-Calculator/releases)

Information on magnus, enemies, and combos: [BKO Documentation](https://docs.google.com/spreadsheets/d/1wXsL9PXnyIuvRiYNgX5p6uTaVBgJhXU1CDzXNFiwLRU/view#gid=1457790647)

![](https://i.imgur.com/9h5XvcK.png)

The above screenshot is a simulation of [this combo](https://youtu.be/n9rcfXrhIZE?t=2503).

## Table of contents <a name="contents"></a>
- [**Party**](#party)
    - [Character icons](#character-icons)
    - [Levels](#levels)
    - [Auras](#auras)
    - [Equipped magnus](#equipped-magnus)
- [**Enemy**](#enemy)
    - [Target](#target)
    - [Knockdown and knockout](#knockdown-and-knockout)
    - [Final phase](#final-phase)
    - [Shield](#shield)
    - [Secondary target](#secondary-target)
    - [HP and effective HP](#hp-and-effective-hp)
    - [Down](#down)
    - [Status](#status)
    - [Armor durability](#armor-durability)
- [**Buttons**](#buttons)
    - [Deck](#deck)
    - [Quest magnus](#quest-magnus)
    - [Temporary boost](#temporary-boost)
    - [Dolphin](#dolphin)
    - [Spreadsheet](#spreadsheet)
    - [Settings](#settings)
    - [MP](#mp)
    - [Next combo](#next-combo)
- [**Cards**](#cards)
    - [Hand](#hand)
    - [Combo](#combo)
- [**Enemy mode**](#enemy-mode)
    - [Aura effects](#aura-effects)
    - [Quest magnus effects](#quest-magnus-effects)
    - [Armor effects](#armor-effects)
    - [Attack effects](#attack-effects)
    - [Additional calculations](#additional-calculations)
- [**Tips**](#tips)
    - [Mouse buttons](#mouse-buttons)
    - [Mouse wheel](#mouse-wheel)
    - [Keyboard shortcuts](#keyboard-shortcuts)
- [**Results table**](#output-table)
- [**Advanced mechanics**](#advanced-mechanics)
    - [Regalia hits](#regalia-hits)
    - [Knockdown hits](#knockdown-hits)


## Party

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


## Enemy

### Target
![](https://i.imgur.com/M8repeT.png)  
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
![](https://i.imgur.com/f6lmZUm.png)  
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


## Buttons

### Deck
![](https://i.imgur.com/dQmxp1I.png)  
Click the Weak Attack magnus to open a deck editor. Here, you can select which cards will show up in each character's hand.  
Each row of magnus forms a group which you can enable or disable using the plus or minus button next to it. You can also remove a magnus from your deck by middle-clicking it in the main window.

![](https://i.imgur.com/RK4B8ku.png)

### Quest magnus
![](https://i.imgur.com/CdmQDLP.png)  
Click the blank magnus to open the quest magnus window. Here, you can add or remove any quest magnus to change your inventory. Some quest magnus add extra damage, allow for critical hits, or change all characters' levels. Magnus with no relevant effects are transparent.

Clicking the "Reset" button will turn all quest magnus into blank magnus. You can also right-click magnus in your inventory to move them around.

![](https://i.imgur.com/nNtreRJ.png)

### Temporary boost
![](https://i.imgur.com/65ylqfB.png)  
Some battle magnus increase or decrease a character's offense or defense for two turns. You can change the boost target by clicking any of your party members. Clicking any of the first 8 magnus will then add a bonus to their offense. (Right-click the magnus to undo this action.) The top text box displays the offense increase for the current turn, and the bottom text box displays the offense increase for the next turn.

Clicking "Next turn" will advance the character to the next turn so that the top value is overwritten by the bottom value, which is then reset to 0. Clicking "Reset" will reset both values to 0.  
You can use the mouse wheel to increase or decrease both boost values by 1.

Notes:  
- One offense turn is any turn from the action bar in the top left corner of the screen. Selecting or discarding magnus does not count as such.  
- One defense turn is a combo of any length performed on an enemy, regardless of whether or not it's a relay combo.  
- When it comes to enemies' offense boosts, only their physical offense can affect the damage they receive. This comes into play when attacking with Firedrake Regalia or Aetherdrake Regalia equipped.
- In the game, the effects of Berserker Drink cannot be amplified with multiple uses.

![](https://i.imgur.com/NJDAoYg.png)

### Dolphin
![](https://i.imgur.com/zRxfMiW.png)  
If the game is running in Dolphin 5.0 or 4.0.x (stable versions only), clicking this icon will import the following data from the emulator:
- Game version
- Levels
- Auras
- Quest magnus
- Target
- HP
- Temporary boost
- Combo
- Deck class and MP (only if MP window is open)

These are not read from Dolphin:
- Equipped magnus
- Down
- Status
- Armor durability


### Spreadsheet
![](https://i.imgur.com/YcdbLcT.png)  
Clicking this button will copy the entire results table to the clipboard. You can then paste it into the spreadsheet software of your choice. Note that the copied data is in plain text and contains cell references which only work if pasted at A1. After pasting it, you can move the table anywhere you want.

This feature allows you to experiment beyond the limitations of the calculator and simulate very specific scenarios, such as blindness misses and auras or quest magnus expiring mid-combo. This is also the only way to simulate hacked conditions or unexplored effects.


### Settings
![](https://i.imgur.com/bXoduDy.png)  
- <i>Auto-close target window:</i> When you choose a target, the target window closes automatically.

- <i>Highlight hits when hovering over an attack card:</i> When you hover over an attack card in your combo, all of its hits in the results table are highlighted in blue.

- <i>Read combo from Dolphin:</i> If this is checked, you can import combos from the game by clicking the Dolphin button.

- <i>Show effective HP remaining:</i> Switch between true HP and effective HP for the last row in the results table. You can also click the row title in the main window to toggle this setting.

- <i>Guillo's English EX combos:</i> The following EX combos are stronger in the English version of the game: Black Yang, White Yin, Fiery Ice Queen, Blazing Glacial Queen, Frigid Queen's Parade, Frigid Queen's Festival

- <i>Guillo's retroactive EX combo bonus:</i> If any of Guillo's standard attacks (except Medium Attack A) directly precedes an EX combo, the projectile will likely hit the target after the EX combo bonus becomes active. This depends on the attack and the distance between Guillo and the enemy.  
For example, if you use Weak Attack, Strong Attack, Icefan, and Sigil Cry in a row, the strong attack may get the 1.3 factor from Ice Queen.

- <i>Secret Queen after enemy gets up:</i> Secret Queen and Secret Queen II are EX combos that can only be triggered if the enemy is down. If the enemy gets up as Milly runs toward them, the first hit will not be a critical hit.

- <i>Saber Dragon Horn:</i> Using this item raises the durability of equipped magnus to their initial durability plus 5. If this setting is checked, the dropdown menus for equipped magnus will allow you to add up to 5 extra durability points.

- <i>Random hits:</i> The attacks listed here are multi-target attacks that randomly hit or miss targets. Using the checkboxes, you can select which hits should connect.

- <i>Show tooltips</i>
	- Variables: If you hover over any of the row titles in the results table, a brief explanation will pop up.
	- Enemies, magnus (secondary windows): Shows the name of the enemy or magnus when hovering over it.

- <i>1st member, 2nd member:</i> Changes the order of party members in the main window and in Temporary Boost.


The checkboxes on the right side allow you to hide or show any of the rows in the results table. You can also hide a row by middle-clicking its title in the main window.

![](https://i.imgur.com/XYyqoYH.png)


### MP

Clicking this button will open a window that lets you simulate MP during battle. As long as this window is open, the program will operate slightly differently:
- The current MP value will be displayed at the end of your combo. When MP reaches 500, a burst button will be shown instead.
- Special attacks can only be selected if you have enough MP.
- Removing cards from your combo with a left-click will revert the MP value to an earlier state. The resulting MP value can only be accurate if MP didn't get auto-capped to its maximum value earlier in the combo. If you remove a card from your combo with a right-click, MP will not be adjusted.

![](https://i.imgur.com/0GNs8Im.png)

Interface elements:
- <i>Deck class:</i> Selecting your deck class changes the MP factor and the max MP. If your deck class is 7 or higher, the max MP is 500.
- <i>MP factor:</i> Most MP mechanics use this factor to increase MP during battle.
- <i>Wingdash:</i> If your heartwing gauge is yellow or orange, you will gain some MP at the start of the battle.
- <i>Reset:</i> Click this button to reset MP to 0.
- MP text box: You can type a new value into this box, or use the mouse wheel to add or subtract the MP gained from a spirit draw or free card.
- <i>MP burst:</i> When MP is 500, you can activate the MP burst.
- MP-charging magnus: These five magnus add different amounts of MP when used. Right-click a magnus to undo the MP charge.
- <i>Include MP from selecting card:</i> The moment you select any of the five MP chargers from your deck (before the character begins their turn), the MP factor is added to the current MP.
- <i>Deviation:</i> A random factor ranging from 0.9 to 1.04 is applied to MP charge and MP drain.
- Artifacts: Using any of these will add the MP factor and reduce MP by 100 times the roman numeral. Right-click an artifact to undo this action.
- <i>MP drain:</i> Hitting an enemy with one of these weapons will steal some of their MP. Note that the amount of MP you gain cannot exceed the amount of MP the enemy has left. Also, since enemy MP is stored as an integer, the amount of MP you gain is an integer as well. Right-click a magnus to undo the MP drain.
- <i>Free card / spirit draw:</i> You can add the MP factor by clicking the plus button. This simulates the effect of selecting a single free card or getting a spirit draw. Click the minus button to undo this action.
- <i>Lightning knockdown:</i> Knocking down an enemy with lightning (+) yields between 30 and 50 MP, while getting knocked down by the enemy (-) takes away between 30 and 50 MP. The exact amount is randomly determined by the game. In the MP window, you can change the amount using the dropdown menu at the bottom. Note that lightning knockdowns don't change the enemy's MP value.



## Cards

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



## Enemy mode

If you click "Switch to enemy" or press X on any window, the program will switch to "enemy mode", which allows you to analyze enemy combos. This mode largely works same way as party mode, but with a few differences:
- In the quest magnus window, only magnus with effects relevant to the current mode are opaque.
- In the temporary boost window, magnus with defense-boosting properties are displayed. Next to the character icons, defense boost values are shown instead of offense boost values.
- In the settings window, the checkboxes on the right side correspond to the rows shown in the current mode.

You can select a target by clicking one of the party member icons on the left side. Alternatively, left-click or right-click the party member icon next to the enemy picture to switch to the next or previous party member, respectively.

An enemy's regular attacks are displayed as standard attack magnus. Special attacks are displayed as "?" magnus.  
Enemies generally use attacks in ascending order of strength (1-2-3) and can skip numbers (1-3) just like party members. However, some enemies only have one or two normal attacks, and so those enemies can use the same attack several times per turn.

Next to the enemy attacks, you can see all armor and accessory magnus available to the current character. Click one of them to equip it, and click it again to unequip it.  

![](https://i.imgur.com/2mQ6cUM.png)

Differences in calculation:
- Enemies have separate values for their offensive power (base offense) and their destructive power (base crush).
- Machina Arma Razer 3 has armor equipped until its HP drops below 50%. As long as the armor is active, it has an offense and crush bonus of 20 (armor bonus). You can turn the armor on and off using the checkbox below the enemy name.
- total_offense = (base_offense * attack_offense * attack_boost_factor * random_offense_factor + armor_bonus) * critical_hit_factor
- total_crush = (base_crush * attack_crush * attack_boost_factor * random_crush_factor + armor_bonus) * critical_hit_factor
- total_defense = ((base_defense + quest_magnus_defense) * (1 - crush_status / crush_limit) * defense_boost_factor * random_defense_factor * vanishing_cloak_factor) + aura_defense
- Damage output can still be 1 (75% chance) even if the equipped armor causes the result to be 0.


### Aura effects

- Guard aura increases all defenses as well as the knockdown threshold, knockout threshold, and crush limit by a set amount*.
- Life aura increases HP by a set amount*.
- Elemental auras increase the defense of their element and reduce the defense of the opposite element by a set amount*.

*The amount depends on the aura level and the character's level.


### Quest magnus effects

- Some quest magnus increase the physical defense for all characters by a set amount. This bonus is added to the base defense.
- Some quest magnus increase the knockdown or knockout threshold for all characters by a certain percentage. This does not affect the Guard aura bonus.
- Some quest magnus increase max HP by a certain percentage. This does not affect the Life aura bonus.


### Armor effects

- Some armor magnus (e.g. Round Shield, Osiris Shield) increase or decrease the knockdown threshold, the knockout threshold, and the crush limit by a certain percentage. This does not affect the Guard aura bonus.
- Vanishing Cloak factor reduces defense by 50%. This does not affect the Guard or elemental aura bonus.
- Confessional Clothes increases max HP by 20%. This does not affect the Life aura bonus.
- Jiraiya's Robe has an 80% chance of preventing critical hits. Click any cell in the "Critical hit factor" row to prevent a critical hit.
- Saizou's Robe prevents all critical hits.
- Heat Camouflage and Hazyfire Camouflage block fire hits.
- Aqua Camouflage and Hazyrain Camouflage block ice hits.
- Heavenbolt Wrap, Heavengale Wrap, and Imperial Ward block all hits.
- Mephistopheles Cloak and its upgrades block the first hit of a combo.


### Attack effects

- Dance King's special attack, "Ring-Around-the-Rosy", has different offense and crush values depending on the number of Prima Queens. You can set this number using the combo box below the enemy name.
- The attacks "Lunchtime" and "Death by Stomping" force mid-combo knockdowns. This causes all follow-up hits to be critical.
- There are other attack effects that have not been implemented yet, such as random critical hits. To simulate those effects, you can click the spreadsheet button, paste the contents of the clipboard into a spreadsheet program at cell A1, and modify the calculation table.


### Additional calculations

- HP = round(round(base_HP * quest_magnus_factor) * armor_factor) + aura_bonus
- knockdown_threshold = round(base_knockdown_threshold * quest_magnus_factor) * armor_factor + aura_bonus
- knockout_threshold = round(base_knockout_threshold * quest_magnus_factor) * armor_factor + aura_bonus
- crush_limit = base_crush_limit * armor_factor + aura_bonus

The rounding of these stats may still differ between the game and this program.



## Tips

### Mouse buttons

Right-clicks and middle-clicks have separate functionality from left-clicks.

Right-click:
- Hand: equip or unequip magnus (works with opaque and transparent magnus)
- Combo: remove a single card from mid-combo
- Quest magnus (inventory): swap positions of two magnus
- Quest magnus (gathering): remove the last instance of a quest magnus from the inventory
- Temporary boost: subtract offense/defense bonus
- MP: subtract MP charge or drain

Middle-click:
- Hand: remove magnus from hand
- Quest magnus (inventory or gathering): remove all instances of a quest magnus from the inventory
- Results table (row title): hide row


### Mouse wheel

You can use the mouse wheel to adjust levels, auras, durability, HP, status, boost, and random deviations.


### Keyboard shortcuts

- 1/2/3: switch characters (main window and Temporary Boost)
- X: switch between party and enemy mode (works from any window)
- T: open target/enemy window
- D: open deck window
- Q: open quest magnus window
- B: open temporary boost window
- E: import data from emulator
- C: copy results table to clipboard
- S: open settings window
- M: open MP window
- N: next combo
- R: resize window to fit results table / remove all quest magnus / reset all boost values / reset MP
- Backspace: remove last card from combo
- Escape: close window (can't be used in main window)


## Results table


<i>Base offense:</i> An offense value based on the party member's level.

<i>Attack offense:</i> An offense value from an attack. Every hit within an attack has its own offense value.

<i>Attack crush:</i> A crush value from an attack. Every hit within an attack has its own crush value.

<i>Attack boost factor:</i> Some magnus grant a 2-turn offense and crush bonus. This factor equals 1 + boost.

<i>Offense deviation:</i> A random offense deviation ranging from -4% to +3%. Click the row title to reset the offense deviation on all hits.

<i>Crush deviation:</i> A random crush deviation ranging from -4% to +3%. Click the row title to reset the crush deviation on all hits.

<i>Electric Helm factor:</i> Bonus factor (1.2) from Electric Helm or Blitz Helm when using lightning attacks.

<i>Weapon offense:</i> Additional offense from equipped weapon.  
Click any cell in this row to enable a mid-combo status effect for specific weapons. Click the row title to remove all mid-combo status effects.

<i>Weapon crush:</i> Additional crush from equipped weapon.  
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

<i>Defense deviation:</i> A random defense deviation ranging from -4% to +3%. Click the row title to reset the defense deviation on all hits.

<i>Total offense:</i> total_offense = (base_offense * attack_offense * attack_boost_factor * random_offense_factor * electric_helm_factor + weapon_offense * element_compatibility * weapon_factor + quest_magnus_bonus + aura_offense) * ex_combo_offense_factor * critical_hit_factor

Total offense can be negative if attack_boost_factor is negative (offense boost < -1).

<i>Total crush:</i> total_crush = (base_offense * attack_crush * attack_boost_factor * random_crush_factor * electric_helm_factor + weapon_crush * element_compatibility * weapon_factor + quest_magnus_bonus + aura_crush) * ex_combo_crush_factor * critical_hit_factor

Total crush can be negative if attack_boost_factor is negative (offense boost < -1).

<i>Total defense:</i> total_defense = base_defense * (1 - crush_status / crush_limit) * defense_boost_factor * random_defense_factor

Total defense can be negative if defense_boost_factor is negative (defense boost < -1).  
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
(knockdown_hit_offense + quest_magnus_bonus) is displayed as "Offense" in the game's menu. It follows these rules:  
- Sagi: knockdown_hit_offense = base_offense * 10  
- Milly: knockdown_hit_offense = base_offense * 7  
- Guillo: knockdown_hit_offense = base_offense * 15

knockdown_hit_offense is rounded to the nearest integer.