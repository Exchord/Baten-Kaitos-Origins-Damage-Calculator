Imports System.Drawing.Imaging
Public Class Main
    Inherits Form

    Dim target_data(13), combo_results, MP_display, dummy, E_target_data(7) As Label
    Dim enemy_HP, E_HP_box As TextBox
    Dim final_phase, shield, secondary_target, down, E_down, E_secondary_target, E_armor As CheckBox
    Dim enemy_status, armor_durability, E_status, E_prima_queens As ComboBox
    Dim target_image, E_target, button(7), hand(300), E_hand(10) As PictureBox
    Dim enemy_mode_button, MP_button, next_combo, burst As Button
    Dim card_panel(3), output_panel, E_output_panel As CustomPanel
    Dim magnus_image(455) As Bitmap
    Public hover As ToolTip
    Dim row_pos(32), E_row_pos(26) As Integer
    Dim description(32), EX_string(102), E_description(26) As String
    Dim highlight As Integer = -1
    Dim dolphin_offset As Integer
    Dim locked As Boolean

    'characters
    Public char_icon(3) As Bitmap
    Dim char_image(3), equipment(3) As PictureBox
    Dim level_selector(3), aura_type(3), aura_level(3), eq_durability(3) As ComboBox
    Dim actual_level(3) As Label
    Dim level(3), aura(3, 2), aura_HP(3), aura_crush_limit(3), aura_down(3), aura_offense(3, 6), aura_crush(3, 6), aura_defense(3, 6) As Integer
    Dim member_max_hp, member_max_hp_armor, member_hp, member_defense(6), member_crush_limit, member_knockdown, member_knockout As Integer
    Dim member_knockdown_armor, member_knockout_armor As Double

    'combo
    Dim combo(125) As PictureBox
    Dim member(125), first_hit(125), equip(125), EX(125), MP_gain(125) As Integer

    'hits
    Dim table(1000, 32), E_table(60, 26) As Label
    Dim hit_modifier(1000, 3), E_hit_modifier(60, 3) As ComboBox
    Dim hit_card(1000), hit_element(1000), shield_break_hit As Integer
    Dim weapon_effect(1000), weapon_boost(1000), qm_crit(1000), weapon_crit(1000), knockdown_hit(1000), min_one(1000), prevent_crit(60) As Boolean

    Dim hits, true_HP, true_max_HP, effective_HP, effective_max_HP, character, armor_defense(6), first_card(64), turns, post_combo_HP, post_combo_armor, post_combo_status, post_combo_equip(3, 2), turns_per_member(3) As Integer
    Public cards, combo_target, item_target, QM_inventory(24), QM_total_bonus(15) As Integer
    Public offense_boost(3, 6, 2), defense_boost(3, 6, 2), enemy_defense_boost(6, 2), enemy_offense_boost(6, 2) As Double
    Dim post_combo_offense_boost(3, 6, 2), post_combo_defense_boost(3, 6, 2), post_combo_enemy_defense_boost(6, 2), post_combo_enemy_offense_boost(6, 2), current_MP As Double
    Dim relay, relay_equip, post_combo_down, post_combo_shield As Boolean
    Public burst_active, enemy_mode As Boolean
    Public deck_magnus(455) As String
    Dim hProcess As IntPtr

    Public ReadOnly magnus_name() As String = {"This number is blank", "Weak Attack", "Medium Attack", "Strong Attack", "Weak Attack B", "Medium Attack B", "Strong Attack B", "Mirage Turn", "Pegasus Jump", "Canyon Wind", "Rabbit Dash", "Scension", "Heavenfall", "Cliffsunder", "Ascension", "Red Padma", "Icebloom", "Transcension", "Blast Tooth", "Rime Blade", "The Godling's Rapture", "Swallowtail", "Emerald Thrush", "Sevenstar Dust", "Arabesque", "Diamond Drop", "Rising Condor", "Phoenix Dive", "Open Your Eyes", "Firewheel", "Icefan", "Fulgadrum", "Ghostarrow", "Sigil Cry", "Twin Ice Auger", "Levinsnake's Rise", "Spirilight Quiver", "Wickedwing Revels", "Heavenlapse", "Empyreal Wildfire", "Zeniver Cascade", "Lightendrake's Drop", "Fellstar Gleam", "Shadowflame Engine", "Aphelion Dustwake", "Punk Knife", "Longsword", "Saber", "Freesword", "Marvelous Sword", "Arondite", "Apocalypse Sword", "Flame Sword", "Flametongue", "Sword of Thirst", "Kusanagi", "Efreeti Saber", "Prominence Sword", "Laevateinn the Flameking", "Ice Dagger", "Crystal Edge", "Aqua Truth", "Sword of Tears", "Siren Sword", "Frozen Sword", "Flash Dagger", "Thunderfish", "Glimmer", "Jupiter Sword", "Plasma Blade", "Ray of Truth", "Apostolos", "Ascalon", "Murderous Joker", "Ravensbrood", "Cutthroat Knife", "Greatsword", "Sleepsword", "Victory Sword", "Excalibur", "Longfire", "Fire Saber", "Scorching Sword", "Blaze Sword", "Blazetongue", "Flametongue Rekindled", "Desert Moon", "Parched Sword", "Amenohabagiri", "Efreeti Heart", "Efreeti Blade", "Fiery Apocalypse", "Sol Sword", "Helios Sword", "Ice Soldier", "Ice Saber", "Crystalsword", "Crystal Saber", "Aqua Brand", "Sword of Destiny", "Mermaid Sword", "Silent Sword", "Icy Apocalypse", "Frozen Edge", "Thundersword", "Thunderfish Rex", "Thunder Edge", "Thunderslash", "Levinlight", "True Glimmer", "Galantyne", "Jupiter's Rage", "Gaea Sword", "Thunderous Apocalypse", "Coruscant Blade", "Rem Truth", "El Truth", "Galahad", "Apostolos Duo", "Divine Apocalypse", "Dragonbuster", "Double Joker", "Murderous Soul", "Dark Apocalypse", "Ravensblood", "Wooden Club", "Panther Bludgeon", "Rose Shill", "Classic Cudgel", "Golden Cudgel", "Heat Club", "Drakeshead Stave", "Efreeti Horn", "Crystal Cudgel", "Glacial Bludgeon", "Cold Queen", "Lightning Club", "Luciferous Stave", "Wild God", "Vajra the Indestructible", "White Club", "Demon Cudgel", "Rosevine", "Classic Impact", "Heat Cudgel", "Poison Heat", "Fire Panther", "Drakeslord Stave", "Efreeti Tusk", "Ice Club", "Crystal Blue", "Glacial Mass", "Diamond Queen", "Queen's Breath", "Thunder Club", "Lightning Strike", "Thunder Panther", "Auriferous Stave", "Plasma Cudgel", "Astratheos", "Holy Cudgel", "White Wish", "Cerberus Cudgel", "Sorcery Sophia", "Hidden Sophia", "Mars Sophia", "Idios Pyr", "Ice Roue", "Apsu Sophia", "Deluge the Seabane", "Thunder Roue", "Thor Sophia", "Light Sophia", "Fair Guardian", "Cross Sophia", "Hades Sophia", "Fire Sophia", "Burning Revelation", "Meteor Sophia", "Deuteros Pyr", "Ice Sophia", "Ice Sense", "Apsu Logos", "Thunder Sense", "Thor Pathos", "Holy Vein", "Sophic Guardian", "Cross Sagesse", "Hades Sagesse", "Leather Vest", "Full Plate", "Chain Mail", "Scale Mail", "Battle Suit", "Golden Armor", "Amor Alma", "Flame Mail", "Salamander Tongue", "Snow Mail", "Breath Jacket", "Armor of Tonitrus", "Saint's Armor", "Evil Mail", "Pow Vest", "Crocodile Vest", "Solid Plate", "Heavy-Duty Plate", "Lucentskin Mail", "Crossbone Mail", "Grand Armor", "Queen Armor", "Divine Armor", "Passio Alma", "Amatrix Alma", "Blaze Mail", "Flame Armor", "Flamelink Mail", "Fiery Battle Suit", "Salamander Eyes", "Salamander Scales", "Icy Battle Suit", "Blizzard Mail", "Snowwind Mail", "Levinlink Mail", "Temper of Tonitrus", "Fires of Tonitrus", "Ice of Tonitrus", "Thunder of Tonitrus", "Light of Tonitrus", "Shadow of Tonitrus", "Grandmail of Tonitrus", "Saint-Knight's Armor", "Saint-Champion's Armor", "Brilliant Armor", "Karma Mail", "Chaos Mail", "Grappler's Gi", "Twelve-Layered Kimono", "Firedrake Regalia", "Heat Camouflage", "Frozen Suit", "Aqua Camouflage", "Heavenbolt Wrap", "Jiraiya's Robe", "Sister's Habit", "Mourning Dress", "Fighter's Gi", "Warrior's Gi", "Fifteen-Layered Kimono", "Aetherdrake Regalia", "Hazyfire Camouflage", "Diamond Suit", "Hazyrain Camouflage", "Heavengale Wrap", "Saizou's Robe", "Mother Superior's Habit", "Confessional Clothes", "Mournful Dress", "Heat Robe", "Robe of Firelight", "Pyre Frock", "Nixie Garb", "Sleet Shawl", "Freezing Gown", "Thunder Robe", "Thunderhead Cloak", "Holy Shroud", "Mephistopheles Cloak", "Sublime Garb", "Flame Robe", "Indigo Flame Robe", "Firedawn Robe", "Firemoon Robe", "Burning Pyre Frock", "Canonical Pyre Frock", "Undine Garb", "Snowstorm Shawl", "Jinni's Frost", "Iceberg", "Frozen Gown", "Lightning Robe", "Thundergod Cloak", "Whitebolt Cloak", "Blackbolt Cloak", "Holy Fireshroud", "Holy Lightshroud", "Holy Vigilshroud", "Flamistopheles Cloak", "Levistopheles Cloak", "Inferno Cloak", "Sacrosanct Robes", "Garb of Atonement", "Garb of Thorns", "Hero Mask", "Power Helmet", "Golden Helm", "Hell-Purged Casque", "Phoenix Helm", "Crystal Helm", "Kappa Helmet", "Electric Helm", "Helm of Indra", "Angel's Helm", "Skull Mask", "Negative Hat", "Mk.II Mask", "Heavy-Duty Helmet", "Justice Helm", "Divine Helm", "Golden Knightshelm", "Hell-Baked Casque", "Phoenix Head", "Phoenix Heart", "Glacial Helm", "Kappa Helmet, Jr.", "Blitz Helm", "Grandhelm of Indra", "War Helm of Indra", "Innocent's Helm", "Foxfire Cap", "Flame Hood", "Scarlet Crown", "Zelos Kune", "Frost Cap", "Aqua Hood", "Crown of Bubbles", "Quickfreeze Cap", "Thunder Hat", "Indiglow Cowl", "Welkinsire Cap", "Nurse's Cap", "Hymnos Kune", "Trigon Band", "Sparkfire Cap", "Blaze Hood", "Ardor Kune", "Zelos Kapelo", "Icicle Cap", "Marine Hood", "Crown of Seaspray", "Crown of Ice Pearls", "Big Thunder Hat", "Thunderhead Cowl", "Astral Cowl", "Thunderpeal Cap", "Welkinswrath Cap", "Mary's Cap", "Happiness Kune", "Rock Kune", "Gloam Band", "The Adversary's Band", "Round Shield", "Buckler", "Tower Shield", "Battle Shield", "Platinum Shield", "Versed Shield", "Whitecap Shield", "Thunderer's Shield", "Anubis Shield", "Coin Shield", "Strongshield", "Devil Tower Shield", "Mount Shield", "Valor Shield", "Silver Shield", "Mellow Shield", "Shield for Troubled Waters", "Greatcap Shield", "Fulminator's Shield", "Osiris Shield", "Low Potion", "Potion", "Middle Potion", "High Potion", "Adhesive Bandages", "Bandages", "First Aid Kit", "Fate's Cordial", "Red Beans", "Blue Beans", "Yellow Beans", "Black Beans", "Green Beans", "Purple Beans", "Pink Beans", "Herb", "Herb Powder", "Alarm Clock", "Brawn-Brewed Tea", "Fire-Brewed Tea", "Ice-Brewed Tea", "Lightning-Brewed Tea", "Elbow Grease Tea", "Brawn Fruit", "Fire Fruit", "Ice Fruit", "Lightning Fruit", "Rainbow Fruit", "Berserker Drink", "Hot Spring", "Cancerite Booze", "Dragon Claw", "Bomb", "Elixir", "Book of Mana", "Fate Idol", "Fate's Kiss", "Chalice of Freedom", "Catfish King's Whiskers", "Herb Flower", "White Night Beans", "Poison Ashes", "Harp of Slumber", "Hero's Crest", "Emperor's Crest", "The Beast's Chains", "The Beast's Collar", "The Beast's Shackles", "Saber Dragon Horn", "Pegasus Feather", "Migraine Mirror", "Tarot Card: Death", "Moon-Shaped Earring", "Star-Shaped Earring", "Sword-Shaped Earring", "Cross Pendant", "Hermit's Cane", "Warrior's Scarf", "Pegasus Anklet", "Imperial Ward", "Vanishing Cloak", "Crimson Love", "Azure Lotus", "Citrine Arc", "Ebon Slash", "True Verdure", "Violet Taboo", "Purest Gold", "Taunt", "Will", "Force", "Escape", "Fire Element", "Ice Element", "Thunder Element", "Light Element", "Dark Element", "Flare Element", "Freeze Element", "Plasma Element", "Holy Element", "Shade Element", "Toxic Dumpling", "Mattress"}
    ReadOnly magnus_type() As Integer = {0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 9, 9, 9, 9, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 7, 7}
    ReadOnly magnus_user() As Integer = {0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    ReadOnly relay_mark() As Boolean = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0}
    ReadOnly spirit_number() As Integer = {0, 2, 4, 6, 2, 4, 6, 3, 3, 5, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 10, 7, 7, 7, 8, 8, 8, 9, 9, 7, 7, 7, 7, 8, 8, 8, 8, 8, 9, 9, 9, 9, 9, 9, 10, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0}
    ReadOnly MP_cost() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 1, 1, 1, 2, 2, 2, 3, 3, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    ReadOnly magnus_element() As Integer = {0, 6, 6, 6, 6, 6, 6, 6, 0, 6, 0, 0, 1, 3, 6, 1, 2, 6, 1, 2, 4, 0, 2, 3, 6, 2, 3, 1, 3, 1, 2, 3, 4, 2, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 2, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 4, 5, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 4, 4, 5, 0, 0, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 5, 1, 1, 1, 1, 2, 2, 2, 3, 3, 4, 4, 5, 5, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 3, 3, 4, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 0, 0, 1, 1, 2, 2, 3, 3, 4, 5, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 1, 1, 1, 2, 2, 2, 3, 3, 4, 5, 5, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 5, 5, 5, 5, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 5, 5, 0, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 3, 4, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 4, 4, 5, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 5, 5, 0, 0, 0, 0, 0, 1, 2, 3, 5, 0, 0, 0, 0, 0, 0, 1, 2, 2, 3, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 5, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 0, 0}
    ReadOnly durability() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 8, 8, 18, 18, 10, 10, 6, 6, 5, 10, 10, 10, 0, 12, 12, 6, 15, 15, 15, 5, 7, 8, 15, 15, 10, 10, 10, 5, 5, 5, 8, 18, 18, 0, 8, 8, 4, 6, 6, 6, 10, 5, 10, 10, 10, 10, 10, 10, 12, 8, 18, 12, 6, 15, 15, 15, 10, 15, 18, 7, 7, 12, 12, 0, 13, 20, 15, 10, 10, 10, 10, 12, 0, 10, 0, 5, 5, 10, 5, 6, 20, 8, 20, 20, 12, 12, 12, 16, 16, 16, 9, 12, 16, 0, 15, 12, 8, 20, 12, 12, 20, 12, 12, 5, 16, 16, 16, 16, 9, 9, 20, 0, 20, 16, 20, 15, 12, 5, 10, 5, 5, 7, 7, 0, 5, 7, 7, 7, 5, 5, 5, 10, 7, 7, 5, 7, 7, 7, 7, 7, 7, 5, 5, 5, 5, 7, 0, 8, 8, 10, 6, 8, 10, 8, 7, 8, 7, 8, 5, 8, 7, 0, 10, 13, 8, 8, 15, 10, 8, 6, 7, 8, 8, 8, 8, 10, 10, 7, 7, 7, 7, 7, 7, 7, 11, 12, 8, 8, 7, 7, 7, 10, 8, 7, 8, 7, 5, 10, 7, 8, 7, 7, 10, 8, 7, 8, 7, 5, 10, 7, 7, 8, 6, 8, 8, 7, 8, 8, 6, 8, 8, 4, 8, 6, 6, 8, 8, 8, 12, 7, 8, 8, 13, 8, 6, 8, 8, 8, 8, 8, 8, 4, 4, 4, 8, 8, 8, 5, 5, 8, 8, 8, 13, 8, 10, 10, 8, 8, 8, 5, 10, 8, 8, 8, 8, 8, 15, 13, 8, 10, 10, 10, 8, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 8, 0, 8, 0, 0, 0, 5, 0, 0, 0, 8, 0, 0, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 8, 7, 0, 9, 10, 0, 10, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10, 10, 10, 10, 10, 5, 10, 5, 10, 10, 10, 10, 10, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    ReadOnly weapon_offense() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 15, 28, 33, 50, 83, 93, 20, 25, 40, 62, 68, 95, 110, 7, 15, 30, 38, 56, 76, 10, 18, 47, 85, 51, 25, 53, 80, 99, 75, 20, 22, 33, 50, 90, 15, 28, 20, 30, 25, 35, 40, 53, 62, 68, 77, 93, 95, 100, 7, 28, 33, 23, 45, 38, 70, 56, 93, 87, 33, 18, 18, 18, 47, 47, 83, 85, 92, 93, 80, 25, 65, 83, 53, 93, 80, 99, 99, 93, 75, 17, 53, 25, 45, 75, 20, 43, 78, 28, 40, 74, 25, 43, 90, 100, 42, 45, 25, 45, 30, 20, 53, 60, 87, 20, 32, 40, 82, 74, 20, 36, 65, 43, 75, 95, 85, 42, 45, 10, 45, 23, 73, 12, 50, 91, 18, 47, 36, 55, 35, 60, 15, 58, 50, 80, 15, 27, 69, 30, 68, 43, 70, 35, 77, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 7, 4, 7, 7, 15, 13, 10, 13, 13, 0, 0}
    ReadOnly weapon_crush() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 10, 14, 16, 25, 41, 46, 10, 15, 20, 31, 36, 47, 55, 3, 7, 15, 18, 28, 43, 10, 25, 23, 42, 25, 12, 26, 40, 0, 37, 5, 10, 16, 25, 41, 15, 14, 20, 10, 20, 15, 20, 20, 31, 36, 36, 46, 47, 47, 3, 14, 20, 7, 15, 18, 28, 28, 46, 43, 20, 25, 25, 25, 23, 23, 41, 55, 42, 46, 25, 12, 12, 41, 26, 46, 40, 0, 0, 46, 37, 15, 40, 20, 40, 78, 15, 36, 63, 20, 33, 67, 20, 38, 82, 92, 35, 37, 20, 40, 17, 15, 40, 36, 80, 15, 20, 33, 67, 67, 20, 20, 40, 38, 87, 82, 78, 35, 60, 5, 20, 12, 30, 5, 25, 40, 10, 21, 15, 28, 14, 30, 5, 12, 10, 30, 5, 5, 25, 10, 21, 15, 28, 14, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 7, 13, 7, 7, 10, 13, 20, 13, 13, 0, 0}
    ReadOnly magnus_defense(,) As Integer = {{0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {12, 11, 6, 6, 6, 6}, {22, 15, 8, 8, 8, 8}, {36, 20, 10, 10, 10, 10}, {61, 35, 19, 19, 19, 19}, {80, 50, 25, 25, 25, 25}, {133, 80, 50, 50, 50, 50}, {168, 100, 55, 55, 55, 55}, {9, 20, 6, 6, 6, 6}, {50, 130, 30, 30, 30, 30}, {83, 52, 140, 52, 52, 52}, {40, 30, 30, 77, 30, 30}, {70, 40, 40, 101, 40, 40}, {95, 45, 45, 45, 160, 45}, {80, 55, 27, 27, 27, 96}, {12, 11, 6, 6, 6, 6}, {28, 11, 6, 6, 6, 6}, {22, 15, 8, 8, 8, 8}, {50, 30, 15, 15, 15, 15}, {74, 35, 19, 19, 19, 19}, {61, 35, 19, 19, 19, 19}, {133, 80, 50, 50, 50, 50}, {133, 80, 50, 50, 50, 50}, {146, 80, 50, 50, 50, 50}, {168, 100, 55, 55, 55, 55}, {205, 100, 55, 55, 55, 55}, {40, 66, 33, 33, 33, 33}, {20, 30, 15, 15, 15, 15}, {36, 40, 10, 10, 10, 10}, {92, 93, 25, 25, 25, 25}, {50, 130, 30, 30, 30, 30}, {50, 130, 100, 30, 30, 30}, {92, 50, 93, 25, 25, 25}, {100, 52, 140, 52, 52, 52}, {83, 52, 140, 52, 52, 52}, {36, 20, 10, 40, 10, 10}, {100, 40, 40, 101, 40, 40}, {70, 40, 40, 101, 40, 40}, {70, 40, 40, 101, 40, 40}, {70, 40, 40, 101, 40, 40}, {70, 40, 40, 101, 100, 40}, {70, 40, 40, 101, 40, 40}, {70, 40, 40, 101, 40, 40}, {95, 45, 45, 45, 160, 45}, {130, 45, 45, 45, 160, 130}, {95, 45, 45, 45, 188, 45}, {80, 55, 27, 27, 27, 96}, {200, 200, 200, 200, 200, 230}, {59, 30, 30, 30, 30, 30}, {300, 30, 30, 30, 30, 30}, {40, 81, 40, 40, 40, 40}, {0, 52, 0, 0, 0, 0}, {27, 27, 54, 27, 27, 27}, {0, 0, 52, 0, 0, 0}, {10, 10, 10, 20, 10, 10}, {70, 91, 70, 70, 70, 70}, {24, 24, 24, 24, 48, 24}, {68, 68, 68, 68, 68, 136}, {59, 30, 30, 30, 30, 30}, {59, 30, 30, 30, 30, 30}, {300, 30, 30, 30, 30, 30}, {40, 81, 40, 40, 40, 40}, {0, 52, 0, 0, 0, 0}, {27, 27, 54, 27, 27, 27}, {0, 0, 52, 0, 0, 0}, {10, 10, 10, 20, 10, 10}, {70, 91, 70, 70, 70, 70}, {50, 50, 50, 50, 100, 50}, {60, 60, 60, 60, 120, 60}, {0, 0, 0, 0, 0, 350}, {5, 13, 6, 8, 8, 8}, {15, 52, 25, 30, 30, 30}, {25, 128, 64, 80, 80, 80}, {10, 21, 43, 28, 28, 28}, {17, 26, 57, 35, 35, 35}, {50, 72, 145, 72, 72, 72}, {5, 11, 11, 15, 11, 11}, {35, 40, 40, 60, 40, 40}, {0, 0, 0, 0, 150, 120}, {0, 0, 0, 0, 0, 84}, {37, 90, 90, 90, 80, 161}, {20, 40, 20, 35, 35, 35}, {5, 13, 6, 8, 8, 8}, {25, 110, 55, 80, 80, 80}, {15, 52, 25, 30, 30, 30}, {40, 198, 99, 99, 99, 99}, {25, 128, 64, 80, 80, 80}, {10, 21, 56, 28, 28, 28}, {17, 26, 57, 35, 35, 35}, {50, 72, 180, 72, 72, 72}, {50, 72, 145, 72, 72, 72}, {50, 72, 145, 72, 72, 72}, {5, 11, 11, 35, 11, 11}, {80, 100, 100, 230, 100, 100}, {35, 40, 40, 60, 58, 40}, {35, 40, 40, 60, 40, 58}, {0, 120, 0, 0, 150, 120}, {0, 0, 0, 0, 200, 120}, {0, 0, 0, 0, 150, 120}, {0, 50, 0, 0, 0, 84}, {0, 0, 0, 50, 0, 84}, {0, 0, 0, 0, 0, 96}, {90, 90, 90, 90, 80, 161}, {37, 90, 90, 90, 140, 161}, {37, 90, 90, 90, 80, 240}, {38, 19, 19, 19, 19, 19}, {28, 14, 14, 14, 14, 14}, {83, 41, 41, 41, 41, 41}, {15, 30, 15, 15, 15, 15}, {55, 110, 55, 55, 55, 55}, {30, 30, 60, 30, 30, 30}, {23, 23, 45, 23, 23, 23}, {10, 10, 10, 20, 10, 10}, {25, 25, 25, 50, 25, 25}, {56, 56, 56, 56, 111, 56}, {25, 25, 25, 25, 25, 50}, {0, 0, 0, 0, 0, 250}, {38, 19, 19, 19, 19, 19}, {28, 14, 14, 14, 14, 14}, {83, 53, 41, 41, 41, 41}, {83, 41, 41, 41, 53, 41}, {160, 80, 80, 80, 80, 80}, {36, 72, 36, 36, 36, 36}, {55, 110, 55, 55, 55, 55}, {55, 110, 55, 55, 55, 55}, {65, 65, 130, 65, 65, 65}, {23, 23, 45, 23, 23, 23}, {10, 10, 10, 40, 10, 10}, {70, 40, 40, 80, 40, 40}, {25, 25, 25, 50, 25, 25}, {56, 56, 56, 56, 111, 56}, {0, 77, 0, 0, 0, 0}, {0, 98, 0, 0, 0, 0}, {0, 176, 0, 0, 0, 0}, {0, 200, 0, 0, 0, 0}, {0, 0, 180, 0, 0, 0}, {0, 0, 112, 0, 0, 0}, {0, 0, 280, 0, 0, 0}, {0, 0, 86, 0, 0, 0}, {0, 0, 0, 107, 0, 0}, {0, 0, 0, 115, 0, 0}, {0, 0, 0, 220, 0, 0}, {0, 0, 0, 0, 83, 0}, {0, 0, 0, 0, 211, 0}, {0, 0, 0, 0, 0, 140}, {0, 77, 0, 39, 0, 0}, {0, 98, 0, 0, 0, 0}, {0, 200, 0, 0, 0, 0}, {0, 300, 0, 0, 0, 0}, {0, 0, 180, 0, 0, 0}, {0, 0, 112, 0, 0, 0}, {140, 0, 280, 0, 0, 0}, {0, 0, 280, 0, 0, 0}, {0, 0, 0, 151, 0, 0}, {57, 0, 0, 115, 0, 0}, {0, 0, 57, 115, 0, 0}, {0, 0, 0, 253, 0, 0}, {0, 0, 0, 220, 0, 0}, {0, 0, 0, 0, 140, 0}, {0, 0, 0, 0, 260, 0}, {0, 0, 0, 0, 211, 0}, {0, 0, 0, 0, 0, 181}, {0, 0, 0, 0, 0, 220}, {45, 0, 0, 0, 0, 0}, {65, 0, 0, 0, 0, 0}, {143, 0, 0, 0, 0, 0}, {86, 0, 0, 0, 0, 0}, {205, 0, 0, 0, 0, 0}, {106, 107, 0, 0, 0, 0}, {39, 0, 40, 0, 0, 0}, {122, 0, 0, 123, 0, 0}, {209, 0, 0, 0, 0, 210}, {50, 0, 0, 0, 0, 0}, {65, 0, 0, 0, 0, 0}, {143, 0, 0, 0, 0, 142}, {143, 0, 0, 0, 0, 0}, {100, 0, 0, 0, 0, 0}, {205, 0, 0, 0, 0, 0}, {106, 107, 0, 0, 0, 0}, {169, 0, 170, 0, 0, 0}, {39, 0, 40, 0, 0, 0}, {122, 0, 0, 123, 0, 0}, {209, 0, 0, 0, 0, 210}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 30, 0, 0, 0, 0}, {0, 0, 30, 0, 0, 0}, {0, 0, 0, 30, 0, 0}, {0, 0, 0, 0, 0, 30}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}}
    ReadOnly armor_crush_limit() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 5, 15, 10, 30, 20, 8, 25, 30, 5, 5, 15, 15, 10, 50, 20, 8, 15, 25, -50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

    Public ReadOnly enemy_name() As String = {"", "Sagi", "Milly", "Guillo", "Empire Grunt", "Imperial Elite", "Dark Serviceman", "Imperial Guard", "Elite Imperial Guard", "Dark Service Peon", "Dark Service Officer", "Fallen Serviceman", "Imperial Swordsman", "Elite Swordsman", "Dark Service Swordsman", "Dark Service Swordmaster", "Imperial Swordguard", "Alpha Paramachina", "Beta Paramachina", "Upgraded Paramachina", "Imperial Battle Machina", "Autonomous Battle Machina", "Masterless Battle Machina", "Cancerite", "Cloud Cancerite", "Mad Cancerite", "Armored Cancerite", "Unuk", "Striper", "Magma Beast", "Shawra", "Blood Leaf", "Badwin", "Filler", "Doomer", "Gormer", "Almer", "Zelmer", "Albireo", "Ray-Moo", "Pul-Puk", "Bar-Mool", "Spell Shellfish", "Magic Shellfish", "Skeleton Warrior", "Undead Swordsman", "Ghoulish Skirmisher", "Devil Claws", "Shadow Claws", "Ghost Claws", "Ceratobus", "Foytow", "Rulug", "Mirabilis", "Lanocaulis", "Acheron", "Maw-Maw-Goo", "Caracal", "Lesser Caracal", "Shadow Caracal", "King Caracal", "Orvata", "Vata", "Balloona", "Fogg", "Armored Balloona", "Slave Balloona", "Alraune", "Queen Alraune", "Ballet Dancer", "Dance King", "Devil's Doll", "Machina Ballerina", "Prima Queen", "Larva Golem", "Cicada Golem", "Ogopogo", "Gigim", "Vodnik", "Juggler", "Master Juggler", "Ahriman", "Mite", "Magician Mite", "Wizard Mite", "Armored Mite", "Goat Chimera", "Phoelix", "Nixie Chimera", "Mobile Turret", "High-Mobility Cannon", "Geryon", "Nebulos", "Monoceros", "Lycaon", "Medium", "Shaman", "Saber Dragon", "Hercules Dragon", "Dragon", "Arma Prototype M", "Hideous Beast 1", "Hideous Beast 2", "Umbra", "Malpercio's Afterling 1", "Malpercio's Afterling 2", "Giacomo 1", "Giacomo 2", "Giacomo 3", "Valara 1", "Valara 2", "Heughes 1", "Heughes 2", "Nasca 1", "Nasca 2", "Machina Arma: Razer 1", "Machina Arma: Razer 2", "Machina Arma: Razer 3", "Promachina Heughes 1", "Promachina Heughes 2", "Machina Arma: Marauder 1", "Machina Arma: Marauder 2", "Cannon", "Cockpit", "Guillo", "Seginus", "Sandfeeder", "Hearteater", "Holoholobird", "Mange-Roches", "Holoholo Chick", "Lord of the Lava Caves", "Rudra", "Promachina Shanath", "Black Dragon", "Wiseman", "Baelheit", "Verus", "Machinanguis A", "Machinanguis B", "Verus-Wiseman", "Holoholo Egg", "Malpercio's Afterling 2 (head)", "Mr. Quintain", "nero_parts", "Machina Arma: Razer"}
    ReadOnly enemy_type() As Integer = {0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 4, 4, 5, 5, 5, 5, 4, 4, 5, 5, 5, 5, 2, 2, 2, 4, 4, 4, 5, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 5, 5, 5, 1, 1, 1, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 1, 1, 3, 3, 3, 5, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 5, 5, 1, 1, 5, 5, 5, 5, 5, 5, 5, 5, 4, 4, 4, 5, 4, 1, 3, 1, 1, 1, 5, 5, 5, 5, 5, 1, 5, 5}
    ReadOnly HP() As Integer = {0, 1655, 1210, 1400, 90, 680, 220, 1010, 1110, 60, 690, 1340, 210, 1030, 600, 730, 1340, 100, 1160, 2010, 160, 640, 1170, 470, 980, 1060, 470, 870, 1550, 610, 230, 480, 680, 590, 410, 980, 840, 950, 360, 470, 470, 830, 710, 950, 820, 920, 980, 510, 620, 890, 710, 410, 790, 1070, 1390, 890, 1190, 100, 710, 760, 670, 910, 1840, 460, 830, 1710, 1660, 2200, 2850, 1120, 1280, 820, 920, 1080, 1470, 1680, 430, 610, 740, 820, 950, 1250, 290, 490, 730, 670, 150, 510, 920, 1300, 1420, 1460, 1660, 2170, 2090, 1260, 1080, 5030, 6090, 2600, 20160, 1140, 2800, 4560, 5580, 5190, 1600, 3700, 5040, 3080, 6180, 1690, 5460, 2000, 4990, 1760, 5080, 11280, 5280, 9910, 4000, 6680, 3340, 2670, 10320, 8100, 2700, 9120, 6780, 10050, 850, 4270, 8540, 8220, 11580, 7920, 10690, 4160, 2910, 1660, 13140, 300, 2360, 60, 100, 3560}
    ReadOnly defense(,) As Integer = {{0, 0, 0, 0, 0, 0}, {47, 54, 27, 34, 40, 27}, {46, 17, 17, 46, 29, 23}, {27, 40, 54, 40, 47, 54}, {6, 7, 6, 6, 6, 6}, {23, 28, 23, 23, 23, 23}, {9, 9, 9, 9, 8, 11}, {34, 41, 34, 34, 34, 34}, {45, 53, 45, 45, 45, 45}, {5, 6, 5, 5, 5, 5}, {26, 32, 26, 26, 16, 37}, {44, 53, 44, 44, 27, 71}, {11, 9, 9, 8, 9, 9}, {56, 47, 47, 47, 47, 47}, {24, 24, 24, 24, 24, 24}, {38, 33, 33, 33, 33, 33}, {75, 53, 53, 53, 53, 53}, {8, 7, 5, 5, 7, 7}, {44, 38, 33, 11, 38, 38}, {73, 64, 64, 27, 64, 64}, {7, 6, 4, 2, 6, 6}, {29, 24, 24, 10, 24, 24}, {63, 52, 21, 52, 52, 52}, {22, 4, 31, 22, 22, 22}, {44, 18, 71, 62, 44, 44}, {48, 38, 57, 48, 29, 67}, {22, 13, 31, 22, 22, 22}, {44, 22, 38, 38, 38, 38}, {65, 56, 56, 56, 56, 56}, {26, 30, 18, 26, 26, 26}, {12, 7, 7, 12, 12, 12}, {25, 20, 10, 25, 25, 25}, {35, 28, 28, 14, 35, 35}, {31, 25, 25, 31, 31, 12}, {18, 37, 7, 15, 11, 18}, {44, 89, 18, 35, 27, 44}, {39, 77, 15, 31, 23, 8}, {43, 86, 17, 35, 26, 9}, {23, 16, 16, 6, 16, 16}, {31, 9, 26, 44, 22, 22}, {13, 3, 15, 10, 13, 13}, {22, 9, 26, 17, 22, 22}, {60, 26, 53, 53, 53, 26}, {78, 52, 69, 69, 69, 35}, {31, 31, 31, 31, 6, 55}, {34, 34, 34, 34, 7, 62}, {36, 36, 36, 36, 7, 66}, {22, 18, 7, 18, 11, 22}, {28, 9, 23, 23, 14, 28}, {40, 33, 33, 33, 13, 60}, {22, 26, 11, 18, 15, 15}, {18, 30, 7, 15, 15, 15}, {44, 15, 36, 29, 29, 29}, {66, 3, 46, 53, 53, 53}, {84, 4, 59, 67, 67, 67}, {66, 20, 46, 20, 46, 46}, {86, 52, 69, 17, 60, 60}, {8, 5, 5, 8, 8, 8}, {26, 20, 7, 26, 26, 26}, {28, 28, 28, 28, 21, 49}, {41, 33, 25, 41, 41, 41}, {23, 19, 13, 19, 19, 19}, {41, 21, 27, 34, 34, 34}, {16, 12, 12, 12, 12, 12}, {31, 39, 13, 22, 22, 22}, {53, 80, 27, 44, 44, 44}, {60, 78, 26, 43, 43, 43}, {55, 21, 21, 55, 55, 55}, {69, 26, 26, 69, 69, 69}, {41, 41, 25, 25, 41, 41}, {51, 51, 51, 20, 51, 51}, {31, 31, 31, 31, 12, 55}, {48, 34, 14, 14, 34, 34}, {49, 49, 49, 20, 49, 49}, {62, 62, 62, 62, 80, 27}, {76, 76, 76, 76, 98, 33}, {26, 13, 23, 18, 18, 18}, {37, 33, 18, 26, 26, 26}, {46, 18, 41, 32, 32, 32}, {31, 31, 31, 31, 12, 55}, {35, 28, 42, 35, 14, 64}, {52, 52, 63, 52, 21, 94}, {10, 5, 15, 10, 10, 10}, {18, 9, 28, 23, 23, 23}, {27, 21, 34, 41, 34, 21}, {25, 25, 25, 41, 25, 16}, {10, 10, 8, 7, 10, 10}, {18, 22, 4, 15, 22, 22}, {42, 50, 50, 8, 50, 34}, {48, 48, 24, 24, 40, 40}, {61, 61, 31, 51, 51, 51}, {46, 46, 46, 46, 23, 70}, {52, 52, 52, 52, 26, 78}, {56, 56, 56, 56, 72, 24}, {53, 53, 53, 96, 96, 96}, {39, 39, 39, 39, 70, 15}, {48, 32, 48, 48, 72, 16}, {73, 64, 64, 73, 73, 73}, {98, 87, 87, 87, 87, 87}, {64, 72, 56, 56, 64, 64}, {120, 120, 120, 120, 72, 120}, {14, 14, 12, 9, 10, 26}, {26, 15, 41, 26, 18, 46}, {37, 59, 26, 37, 30, 66}, {36, 30, 30, 30, 12, 59}, {44, 36, 36, 36, 66, 7}, {15, 13, 11, 11, 11, 11}, {39, 34, 29, 29, 29, 29}, {44, 38, 33, 33, 33, 33}, {40, 30, 30, 50, 30, 30}, {85, 64, 64, 107, 64, 64}, {50, 30, 25, 25, 25, 25}, {102, 61, 51, 51, 51, 51}, {25, 40, 40, 40, 40, 40}, {49, 78, 78, 78, 78, 78}, {28, 28, 14, 14, 28, 28}, {77, 77, 39, 39, 77, 77}, {104, 104, 52, 52, 104, 104}, {57, 29, 57, 57, 57, 57}, {100, 50, 100, 100, 100, 100}, {53, 53, 26, 53, 53, 53}, {95, 95, 48, 95, 95, 95}, {57, 48, 29, 48, 48, 48}, {57, 48, 29, 48, 48, 48}, {195, 195, 195, 195, 195, 98}, {65, 74, 74, 74, 47, 93}, {10, 28, 28, 28, 28, 28}, {89, 53, 53, 53, 44, 44}, {53, 53, 53, 33, 60, 60}, {82, 64, 64, 73, 82, 82}, {33, 26, 26, 10, 33, 33}, {31, 39, 20, 27, 27, 27}, {68, 60, 60, 60, 38, 75}, {65, 65, 65, 57, 41, 82}, {85, 85, 75, 85, 75, 96}, {87, 87, 87, 87, 76, 76}, {87, 98, 87, 87, 109, 44}, {68, 68, 68, 68, 45, 102}, {45, 57, 45, 57, 45, 45}, {45, 57, 45, 57, 45, 45}, {118, 106, 106, 106, 106, 106}, {128, 64, 19, 51, 51, 51}, {44, 36, 36, 36, 7, 66}, {5, 5, 5, 5, 5, 5}, {0, 0, 0, 0, 0, 0}, {66, 66, 33, 33, 66, 66}}
    ReadOnly resistance(,) As Integer = {{0, 0, 0, 0, 0, 0}, {58, 29, 37, 29, 37, 44}, {33, 20, 52, 39, 52, 52}, {51, 44, 29, 44, 22, 29}, {43, 43, 30, 36, 24, 24}, {51, 51, 37, 44, 29, 29}, {44, 44, 32, 38, 25, 25}, {57, 57, 41, 49, 32, 32}, {59, 59, 42, 50, 33, 33}, {42, 42, 30, 36, 24, 24}, {53, 53, 38, 45, 30, 30}, {62, 62, 44, 53, 35, 35}, {44, 44, 25, 38, 32, 19}, {63, 63, 45, 54, 45, 27}, {52, 52, 37, 44, 37, 22}, {53, 53, 38, 46, 38, 23}, {67, 67, 48, 57, 48, 29}, {42, 24, 24, 42, 0, 0}, {53, 46, 15, 53, 0, 0}, {63, 63, 27, 63, 0, 0}, {30, 18, 18, 30, 0, 0}, {44, 37, 15, 37, 0, 0}, {57, 19, 47, 47, 0, 0}, {22, 51, 36, 36, 22, 51}, {27, 71, 62, 44, 27, 62}, {36, 55, 46, 64, 27, 64}, {22, 51, 36, 36, 22, 51}, {31, 53, 53, 53, 46, 38}, {54, 54, 54, 54, 54, 45}, {0, 35, 49, 49, 56, 35}, {19, 19, 32, 32, 0, 39}, {30, 15, 37, 37, 0, 45}, {33, 33, 16, 41, 0, 49}, {32, 32, 39, 16, 0, 47}, {0, 21, 28, 28, 70, 56}, {0, 27, 35, 35, 89, 71}, {0, 34, 34, 8, 84, 68}, {0, 35, 35, 9, 88, 70}, {34, 34, 7, 34, 34, 34}, {22, 43, 0, 36, 36, 36}, {13, 33, 26, 33, 33, 33}, {14, 36, 29, 36, 36, 36}, {40, 40, 64, 16, 48, 48}, {44, 44, 70, 18, 53, 53}, {39, 39, 39, 0, 0, 0}, {41, 41, 41, 0, 0, 0}, {66, 41, 41, 0, 0, 0}, {35, 7, 35, 42, 42, 14}, {7, 37, 37, 44, 44, 15}, {40, 40, 40, 0, 40, 16}, {56, 14, 35, 28, 42, 14}, {63, 14, 35, 28, 28, 28}, {17, 41, 33, 33, 33, 33}, {8, 56, 64, 64, 56, 64}, {9, 61, 70, 70, 61, 70}, {24, 56, 16, 56, 56, 56}, {53, 70, 9, 61, 61, 61}, {19, 19, 25, 31, 19, 19}, {24, 8, 32, 32, 24, 24}, {33, 33, 33, 57, 25, 25}, {34, 26, 43, 43, 26, 26}, {48, 27, 48, 48, 48, 34}, {32, 41, 57, 57, 57, 41}, {32, 32, 32, 32, 45, 32}, {0, 36, 36, 36, 51, 36}, {0, 44, 44, 44, 62, 44}, {0, 44, 44, 44, 61, 44}, {41, 41, 57, 57, 73, 81}, {44, 44, 61, 61, 79, 88}, {43, 26, 26, 43, 0, 0}, {47, 47, 19, 47, 0, 0}, {55, 39, 39, 71, 0, 0}, {57, 16, 16, 41, 0, 0}, {64, 46, 18, 46, 0, 0}, {53, 53, 53, 9, 62, 62}, {58, 58, 58, 10, 67, 67}, {26, 33, 33, 33, 46, 46}, {70, 28, 42, 42, 49, 49}, {37, 73, 51, 51, 51, 51}, {39, 39, 39, 39, 32, 32}, {41, 41, 41, 41, 33, 33}, {47, 47, 47, 47, 47, 47}, {20, 0, 39, 39, 66, 59}, {15, 37, 37, 37, 73, 66}, {24, 41, 49, 24, 81, 73}, {34, 34, 52, 26, 86, 78}, {0, 0, 6, 31, 31, 44}, {0, 0, 7, 35, 35, 49}, {0, 0, 9, 43, 43, 61}, {51, 0, 43, 43, 0, 0}, {56, 0, 47, 47, 0, 0}, {51, 51, 51, 59, 59, 42}, {53, 53, 61, 61, 61, 53}, {60, 60, 60, 51, 51, 60}, {48, 48, 86, 48, 48, 67}, {42, 42, 42, 76, 42, 42}, {34, 43, 43, 77, 43, 43}, {0, 89, 63, 63, 89, 27}, {0, 77, 77, 77, 0, 0}, {0, 68, 51, 60, 0, 68}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {64, 0, 45, 45, 64, 58}, {74, 0, 52, 52, 74, 66}, {76, 0, 53, 53, 76, 69}, {0, 0, 67, 60, 75, 67}, {0, 0, 86, 76, 95, 86}, {0, 0, 45, 60, 75, 67}, {0, 0, 56, 75, 93, 84}, {0, 0, 30, 60, 75, 45}, {0, 0, 37, 73, 92, 55}, {0, 0, 53, 67, 0, 0}, {0, 0, 68, 84, 0, 0}, {0, 0, 75, 94, 0, 0}, {0, 0, 62, 77, 0, 0}, {0, 0, 74, 93, 0, 0}, {0, 0, 60, 76, 0, 0}, {0, 0, 73, 91, 0, 0}, {73, 0, 0, 82, 0, 0}, {73, 0, 0, 82, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 72, 81, 0, 45}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 80, 72, 72, 0, 80}, {0, 89, 80, 80, 0, 89}, {64, 32, 24, 40, 64, 24}, {0, 0, 42, 42, 0, 0}, {0, 0, 50, 0, 0, 0}, {0, 86, 60, 0, 0, 69}, {0, 0, 95, 95, 0, 57}, {0, 96, 96, 96, 0, 0}, {0, 0, 96, 96, 0, 0}, {0, 98, 59, 59, 0, 98}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0}, {30, 30, 30, 30, 30, 30}, {0, 0, 0, 0, 0, 0}, {0, 64, 64, 0, 0, 0}}
    ReadOnly knockdown() As Integer = {0, 745, 678, 671, 40, 300, 100, 450, 400, 30, 270, 480, 100, 510, 250, 280, 590, 40, 350, 620, 40, 310, 720, 230, 480, 520, 230, 500, 890, 350, 90, 190, 280, 320, 200, 480, 410, 470, 180, 230, 100, 170, 700, 940, 480, 540, 580, 150, 180, 260, 250, 150, 290, 700, 910, 440, 590, 60, 260, 280, 330, 440, 900, 190, 340, 720, 700, 0, 0, 660, 850, 240, 270, 400, 1210, 1520, 280, 400, 480, 240, 280, 430, 100, 180, 270, 330, 80, 250, 570, 320, 420, 820, 940, 1600, 1480, 510, 530, 1860, 2270, 1070, 3360, 250, 520, 890, 1390, 0, 280, 690, 780, 650, 1480, 650, 1410, 320, 1340, 0, 0, 2900, 0, 2480, 0, 0, 0, 0, 2010, 1530, 700, 1930, 1310, 1990, 440, 790, 1490, 1650, 2960, 3030, 2270, 1270, 1420, 790, 0, 0, 0, 40, 0, 0}
    ReadOnly knockout() As Integer = {0, 1118, 986, 1043, 60, 450, 150, 670, 600, 40, 400, 720, 150, 760, 370, 420, 890, 50, 530, 930, 80, 470, 1090, 340, 720, 790, 340, 740, 1340, 520, 190, 390, 560, 480, 300, 720, 620, 700, 260, 340, 210, 340, 1040, 1410, 720, 810, 870, 220, 270, 390, 370, 220, 430, 1040, 1360, 650, 880, 90, 390, 420, 500, 660, 1350, 280, 520, 1080, 1050, 0, 0, 990, 1270, 360, 400, 600, 1810, 2270, 420, 590, 720, 360, 420, 650, 160, 270, 400, 500, 120, 370, 850, 480, 640, 1230, 1410, 2400, 2220, 770, 800, 2790, 3410, 1600, 5040, 0, 0, 0, 0, 0, 420, 1030, 1170, 970, 2220, 970, 2120, 490, 2010, 0, 0, 4340, 0, 3720, 0, 0, 0, 0, 3020, 2290, 1050, 2890, 1960, 2980, 650, 0, 0, 2480, 4440, 4540, 3410, 1900, 2140, 1190, 4940, 0, 0, 50, 0, 0}
    ReadOnly crush_limit() As Integer = {0, 1490, 1233, 1490, 90, 600, 200, 900, 790, 60, 540, 960, 250, 1280, 620, 710, 1480, 70, 710, 1240, 90, 750, 1740, 460, 960, 1050, 460, 710, 1280, 490, 190, 390, 560, 640, 400, 960, 820, 940, 350, 460, 280, 460, 2090, 2810, 960, 1080, 1160, 400, 480, 700, 590, 300, 580, 870, 1140, 1740, 2340, 120, 520, 560, 660, 880, 1800, 380, 690, 1450, 1410, 900, 1170, 1320, 1700, 640, 720, 1070, 2890, 3640, 560, 790, 960, 480, 560, 870, 210, 360, 540, 660, 170, 790, 1820, 640, 850, 1640, 1880, 3200, 2960, 1030, 1070, 4970, 6060, 2130, 6720, 500, 1040, 1780, 2780, 2890, 680, 1870, 2120, 1300, 2960, 1040, 2830, 650, 2690, 990, 2670, 5790, 2360, 5510, 1750, 5240, 5240, 3930, 6720, 4080, 1400, 4100, 2610, 4970, 870, 1670, 3180, 3520, 4440, 6060, 5450, 2530, 2850, 1580, 6590, 10000, 2310, 70, 0, 2260}
    ReadOnly enemy_offense() As Double = {0, 4.2, 3.7, 4.2, 1.5, 5, 2.3, 9.5, 10.5, 1, 5.8, 12.4, 2.5, 17, 7.9, 9.8, 21.1, 1, 7.5, 16.6, 1.9, 9.2, 20.6, 8.3, 17.4, 16.1, 8.3, 9.8, 17, 6.4, 2.4, 5.5, 7.9, 6.8, 5.9, 14.9, 13, 14.5, 4.3, 7.1, 2.6, 4.7, 7.3, 12.1, 11.1, 12.3, 13.2, 4.9, 6.2, 9.2, 6.9, 5.4, 11.2, 11, 15.3, 10.1, 14.5, 1.6, 7.3, 7.9, 10.3, 6, 13.3, 3.6, 8.3, 17.4, 16.9, 13.3, 16.9, 16, 20.1, 11.9, 13.3, 17.9, 18.6, 23.1, 4.6, 7.9, 10, 10.2, 11.8, 19.1, 3.3, 6.2, 9.5, 11.4, 1.8, 6.4, 13, 17.8, 23, 15.1, 17.4, 20, 25.6, 8.6, 13.3, 23, 27.7, 17.8, 32.3, 2.6, 5.9, 8.9, 14.7, 16.2, 4.3, 11.8, 13.6, 9.6, 27.1, 9.6, 24.5, 5.5, 22, 6.1, 18.4, 28, 14.9, 28.1, 10.1, 17.4, 13.4, 13.4, 19.2, 19.1, 6.4, 21.6, 12.8, 20.7, 10.1, 9.4, 17.8, 20.6, 30.1, 28.9, 27.7, 25.6, 25.6, 19.2, 31.3, 0, 16.2, 1, 0, 14.7}
    ReadOnly HP_limit() As Double = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 _
            , 0.5, 0.5, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.9, 0.9, 0.9, 0.925, 0.925, 0.95, 0.95, 0, 0, 0.6, 0, 0, 0, 0, 0, 0, 0.5, 0, 0.95, 0, 0, 0.9, 0, 0, 0, 0.9, 0, 0, 0, 0, 0}
    ReadOnly shield_limit() As Double = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 _
            , 0.75, 0.75, 0.75, 0.5, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0.5, 0, 0, 0, 0, 0, 0, 0, 0, 0.5, 0.5, 0.5, 0.5, 0, 0.5, 0.5, 0.5, 0, 0.5, 0, 0, 0, 0.5, 0.5, 0, 0, 0, 0, 0}

    ReadOnly enemy_crush() As Double = {0, 4.2, 3.7, 4.2, 1.1, 6.2, 2.3, 9.5, 10.5, 0.9, 5.8, 12.4, 1.8, 13.1, 5.2, 7.5, 16.5, 1, 7.5, 16.6, 1.6, 7.9, 17.7, 8.3, 17.4, 16.1, 8.3, 9, 15.7, 5.9, 3, 6.9, 9.8, 8.5, 5.9, 14.9, 13, 14.5, 4.3, 5.9, 2, 3.5, 7.3, 12.1, 10.2, 11.4, 12.2, 3.9, 5, 7.3, 7.9, 4.4, 9.1, 9.2, 13, 10.1, 14.5, 1.6, 7.3, 7.9, 10.3, 6.8, 15.2, 4.1, 8.3, 17.4, 16.9, 11.4, 14.5, 13.7, 17.3, 10.2, 11.4, 16.5, 17.4, 21.5, 4.6, 7.9, 10, 8.5, 9.8, 16.2, 2.6, 5, 7.6, 9.2, 2, 5.4, 13, 17.8, 23, 15.1, 17.4, 22.2, 25.6, 10.8, 11.1, 23, 27.7, 17.8, 35.7, 3, 6.6, 9.8, 16.4, 16.2, 4.3, 12.4, 14.3, 9.6, 27.1, 11, 24.5, 6.9, 22, 6.1, 18.4, 28, 14.1, 28.1, 13, 16.1, 13.4, 13.4, 19.2, 18.3, 6.4, 21.1, 12.8, 23, 11, 8.9, 20.9, 20.6, 30.1, 28.9, 26.1, 22.4, 25.6, 20.8, 31.3, 0, 16.2, 1, 0, 14.7}
    ReadOnly enemy_knockdown_offense() As Integer = {0, 42, 26, 63, 15, 50, 23, 95, 105, 10, 58, 124, 25, 170, 79, 98, 211, 10, 75, 166, 19, 92, 206, 83, 174, 161, 83, 98, 170, 64, 24, 55, 79, 68, 59, 149, 130, 145, 43, 71, 26, 47, 73, 121, 111, 123, 132, 49, 62, 92, 69, 54, 112, 110, 153, 101, 145, 16, 73, 79, 103, 60, 133, 36, 83, 174, 169, 133, 169, 160, 201, 119, 133, 179, 186, 231, 46, 79, 100, 102, 118, 191, 33, 62, 95, 114, 18, 64, 130, 178, 230, 151, 174, 200, 256, 86, 133, 230, 277, 178, 323, 26, 59, 89, 147, 162, 43, 118, 136, 96, 271, 96, 245, 55, 220, 61, 184, 280, 149, 281, 152, 261, 134, 134, 192, 287, 64, 216, 128, 207, 101, 94, 178, 206, 301, 289, 277, 256, 256, 192, 313, 0, 162, 10, 0, 147}

    ReadOnly enemy_attack_data(,,) As Single = {{{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 4, 1048599, 37, 37, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 1048599, 40, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 5, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 0, 19, 10, 10, 0, 19, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 4, 1048599, 37, 37, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 1, 19, 15, 15, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 4, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 19, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 3, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 3, 3145751, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 3, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 3, 3145751, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 10, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 18, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 23, 5, 5, 0, 23, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 4, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 4, 0, 23, 7, 7, 0, 23, 7, 7, 0, 23, 7, 7, 0, 23, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 23, 5, 7, 0, 23, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 4, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 4, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 23, 5, 7, 0, 23, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 4, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 4, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 5, 7, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 2, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 2, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 2, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 2, 2, 19, 20, 20, 2, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 2, 5, 19, 20, 20, 5, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 2, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 2, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 1048599, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 1048599, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 2097175, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 2097175, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 2097175, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 4, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 4, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 4, 2097175, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{201, 1, 1, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{200, 1, 1, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {201, 1, 1, 2097191, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 3, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 3, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 3, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 5, 1, 3145751, 5, 5, 1, 3145751, 5, 5, 1, 3145751, 10, 10, 1, 3145751, 10, 10, 1, 3145751, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 5, 0, 3145751, 5, 5, 0, 3145751, 5, 5, 0, 3145751, 10, 10, 0, 3145751, 10, 10, 0, 3145751, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 1048599, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 4, 0, 2097175, 10, 10, 0, 2097175, 10, 10, 0, 2097175, 10, 10, 0, 2097175, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 5, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 0, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 0, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1003, 1, 3, 1048615, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {101, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 22, 22, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {101, 1, 1, 23, 15, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 20, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 25, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {101, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 22, 22, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 0, 23, 10, 15, 0, 23, 10, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1003, 3, 0, 1048647, 30, 30, 0, 1048647, 30, 30, 0, 1048647, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 0, 23, 10, 15, 0, 23, 10, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1003, 3, 0, 1048647, 30, 30, 0, 1048647, 30, 30, 0, 1048647, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 4, 0, 23, 10, 10, 0, 23, 10, 10, 0, 23, 10, 10, 0, 23, 10, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 4, 1, 23, 15, 10, 1, 23, 15, 10, 1, 23, 15, 10, 1, 23, 15, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 3, 5, 23, 5, 5, 5, 23, 5, 5, 5, 23, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 3, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 3145751, 70, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 3, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 3145751, 70, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 2, 1048615, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 8, 4, 39, 5, 7, 4, 39, 5, 7, 4, 39, 5, 7, 4, 39, 5, 7, 4, 39, 5, 7, 4, 39, 5, 7, 4, 39, 5, 7, 4, 39, 5, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 2, 1048615, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 2, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 2, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 2, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {200, 1, 1, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {202, 1, 2, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 3, 5, 1048595, 10, 10, 5, 1048599, 10, 10, 5, 1048599, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {203, 1, 2, 2097175, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {202, 1, 2, 39, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 3, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {205, 1, 3, 39, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 3, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 1048615, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 2097175, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {200, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 1048615, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {203, 1, 2, 2097175, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 16, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5, 1, 135, 5, 5}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {204, 1, 3, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {205, 1, 3, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 4, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 4, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 2, 3, 23, 20, 20, 3, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 3, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 4, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 4, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 39, 5, 7, 0, 39, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 3, 2097191, 20, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 4119, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 5, 1048599, 50, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 5, 6291479, 60, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 39, 35, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 2, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 2, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 2, 2, 23, 20, 20, 2, 1048599, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 2097191, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 1, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 1048599, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 5, 1048599, 10, 15, 5, 1048599, 10, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 4, 5, 2097175, 5, 5, 5, 2097175, 5, 5, 5, 2097175, 10, 10, 5, 2097175, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{201, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {203, 1, 2, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {205, 1, 3, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 5, 23, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 5, 4, 1048599, 8, 8, 4, 1048599, 8, 8, 4, 1048599, 8, 8, 4, 1048599, 8, 8, 4, 1048599, 8, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 40, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 23, 5, 5, 0, 23, 6, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 3, 0, 23, 5, 5, 0, 23, 6, 6, 0, 23, 6, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 6, 0, 23, 5, 5, 0, 23, 10, 10, 0, 23, 10, 10, 0, 23, 15, 15, 0, 23, 20, 20, 0, 2097175, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 11, 3, 23, 1, 2, 3, 23, 5, 6, 3, 23, 2, 3, 3, 23, 5, 6, 3, 23, 3, 4, 3, 23, 5, 6, 3, 23, 5, 6, 3, 1048599, 6, 7, 3, 1048599, 10, 10, 3, 1048599, 10, 10, 3, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 23, 5, 5, 0, 23, 6, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 3, 0, 23, 5, 5, 0, 23, 6, 6, 0, 23, 6, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 6, 0, 23, 5, 5, 0, 23, 10, 10, 0, 23, 10, 10, 0, 23, 15, 15, 0, 23, 20, 20, 0, 2097175, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 11, 3, 23, 1, 2, 3, 23, 5, 6, 3, 23, 2, 3, 3, 23, 5, 6, 3, 23, 3, 4, 3, 23, 5, 6, 3, 23, 5, 6, 3, 1048599, 6, 7, 3, 1048599, 10, 10, 3, 1048599, 10, 10, 3, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 4114, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 3, 0, 18, 6, 6, 0, 18, 6, 6, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 0, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 4114, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 3, 0, 18, 6, 6, 0, 18, 6, 6, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 0, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 18, 10, 10, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 5, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 2, 0, 18, 10, 10, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 5, 1048599, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 23, 50, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 39, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 6, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 8, 8, 0, 23, 10, 10, 0, 23, 15, 15, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 23, 45, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 0, 39, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 6, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 8, 8, 0, 23, 10, 10, 0, 23, 15, 15, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 23, 45, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 4116, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 3, 0, 23, 5, 10, 0, 23, 10, 10, 0, 23, 15, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 1, 3145751, 60, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 4116, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 3, 0, 23, 5, 10, 0, 23, 10, 10, 0, 23, 15, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 1, 3145751, 60, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 3, 3, 39, 10, 20, 3, 39, 15, 25, 3, 1048615, 20, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 23, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 3, 3, 39, 10, 20, 3, 39, 15, 25, 3, 1048615, 20, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 15, 7.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 4, 23, 20, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 4, 23, 30, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 9, 4, 135, 15, 15, 4, 135, 20, 20, 4, 135, 15, 15, 4, 135, 20, 20, 4, 135, 15, 15, 4, 135, 20, 20, 4, 135, 15, 15, 4, 135, 20, 20, 4, 135, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 6, 4, 23, 10, 30, 4, 23, 5, 15, 4, 23, 5, 10, 4, 23, 5, 5, 4, 23, 10, 5, 4, 23, 25, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 9, 5, 135, 10, 10, 5, 135, 10, 10, 5, 135, 10, 10, 5, 135, 10, 10, 5, 135, 15, 15, 5, 135, 15, 15, 5, 135, 20, 20, 5, 135, 20, 20, 5, 135, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 4, 5, 23, 10, 10, 5, 23, 10, 10, 5, 23, 15, 15, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 0, 23, 10, 15, 0, 23, 12, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 4, 0, 1048599, 5, 5, 0, 1048599, 5, 5, 0, 1048599, 10, 10, 0, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 3, 0, 6299668, 5, 5, 0, 6299668, 10, 10, 0, 6291476, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 0, 23, 10, 15, 0, 23, 12, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 4, 0, 23, 5, 5, 0, 23, 10, 10, 0, 23, 10, 10, 0, 1048599, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 4119, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 4119, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 2, 0, 39, 10, 10, 0, 1048615, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 4119, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 4119, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 2, 0, 39, 15, 15, 0, 1048615, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1002, 1, 0, 23, 50, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 5, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 5, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 19, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 1, 19, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 1, 19, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048595, 45, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 5, 23, 10, 10, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 5, 1048615, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 3, 5, 23, 10, 10, 5, 23, 10, 10, 5, 23, 30, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 2, 0, 23, 10, 10, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 3, 0, 23, 10, 10, 0, 23, 20, 20, 1, 23, 20, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 2, 5, 23, 7, 10, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 10, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 1, 135, 10, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 3145751, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 5, 39, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 4, 4194343, 10, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 1, 1048615, 40, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 5, 4, 23, 5, 5, 4, 23, 8, 8, 4, 23, 10, 10, 4, 23, 15, 15, 4, 2097175, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 5, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 5, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 5, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 5, 5, 8215, 5, 5, 5, 8215, 5, 5, 5, 8215, 8, 8, 5, 8215, 10, 10, 5, 23, 20, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 3, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1002, 5, 0, 135, 5, 10, 0, 135, 5, 10, 0, 135, 8, 10, 0, 1048711, 10, 10, 0, 1048711, 12, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 1, 23, 15, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 3, 23, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 12, 12, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1002, 5, 0, 135, 5, 20, 0, 135, 5, 20, 0, 135, 8, 20, 0, 1048711, 10, 20, 0, 1048711, 20, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 16, 1, 135, 10, 10, 3, 135, 10, 10, 1, 135, 10, 10, 3, 135, 10, 10, 1, 135, 10, 10, 3, 135, 10, 10, 1, 135, 10, 10, 3, 135, 10, 10, 1, 135, 10, 10, 3, 135, 10, 10, 1, 135, 10, 10, 3, 135, 15, 15, 1, 135, 15, 15, 3, 135, 15, 15, 1, 1048711, 15, 15, 3, 1048711, 20, 20}, {1000, 10, 1, 135, 10, 10, 1, 135, 10, 10, 3, 135, 10, 10, 1, 135, 10, 10, 1, 135, 10, 10, 3, 135, 10, 10, 3, 135, 15, 15, 1, 135, 15, 15, 1, 1048711, 15, 15, 3, 1048711, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 4, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 10, 4, 5243015, 5, 5, 4, 5243015, 5, 5, 4, 5243015, 5, 5, 4, 5243015, 8, 8, 4, 5243015, 8, 8, 4, 5243015, 10, 10, 4, 5243015, 10, 10, 4, 5243015, 15, 15, 4, 5243015, 15, 15, 4, 5243015, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 1, 4, 2097191, 10, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 18, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 18, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 18, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 3145751, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}} _
, {{101, 1, 0, 23, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {102, 1, 0, 23, 15, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {103, 1, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 1048615, 30, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1001, 6, 0, 23, 5, 5, 0, 23, 5, 5, 0, 23, 8, 8, 0, 23, 10, 10, 0, 23, 15, 15, 0, 23, 20, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1000, 1, 3, 23, 50, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}}}

    ReadOnly enemy_attack_name(,) As String = {{"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "Electro Shot", "", "", ""}, {"", "", "Blinder Shot", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "Slumber Shot", "", "", ""}, {"", "", "Slumber Shot", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Shock Bomb", "", ""}, {"", "", "", "Fryercracker", "", ""}, {"", "", "", "Shock Bomb", "", ""}, {"", "", "", "Fryercracker", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Saber Hand", "", ""}, {"", "", "", "Saber Hand", "Rampage Chop", ""}, {"", "", "", "", "", ""}, {"", "", "", "Force Ring Alpha", "", ""}, {"", "", "", "Force Ring Beta", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Collar Lop", "", ""}, {"", "", "", "Headhunter Chop", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Inferno", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Enervating Waves", "", ""}, {"", "", "", "Toxic Waves", "", ""}, {"", "", "", "Snooze Waves", "", ""}, {"", "", "", "", "", ""}, {"", "Flamethrower", "", "", "", ""}, {"Foxfire", "", "", "", "", ""}, {"Foxfire", "Seafire", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Electro Crash", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Dynamite Rush", "", ""}, {"", "", "", "Bloodsucker Blaze", "", ""}, {"", "", "", "Bloodsucker", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Shell Spin", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Uber Cat Kick", "", ""}, {"", "", "", "Uber Cat Kick", "", ""}, {"", "Hover Strike", "", "", "", ""}, {"", "Hover Strike", "Earsplitting Whoop", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Poison Breath", "", ""}, {"", "", "", "Poison Breath", "Beastprodder", "Beastprodder (Overheat)"}, {"", "", "", "", "", ""}, {"", "", "", "Ring-Around-the-Rosy", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Self-Destruct", "", ""}, {"", "", "", "Self-Destruct", "", ""}, {"", "White Breath", "", "", "", ""}, {"", "Beam Barrage", "White Breath", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Armadillo Attack", "", ""}, {"", "", "", "Armadillo Attack", "", ""}, {"", "Pyromancy", "", "", "", ""}, {"", "Cryomancy", "", "", "", ""}, {"", "Knife Parade", "Crystal Cryomancy", "", "", ""}, {"", "", "", "", "", ""}, {"", "Ice Rubble", "", "", "", ""}, {"", "Thunderbolt", "", "", "", ""}, {"", "All-Or-Nothing Thunder", "", "", "", ""}, {"", "Supersonic Voice", "", "", "", ""}, {"", "Incendiary Voice", "", "", "", ""}, {"", "Supersonic Voice", "Chilling Voice", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Fusillade", "", ""}, {"", "", "Evil Thunder", "", "", ""}, {"", "", "", "Devil Thunder", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Electro Horn", "", ""}, {"", "Numinous Wings", "", "", "", ""}, {"", "Numinous Wings", "", "", "", ""}, {"", "", "", "Crimson Hail", "", ""}, {"", "Hercules Laser", "", "", "", ""}, {"", "Conflagrant Breath", "Skywrenching Roar", "", "", ""}, {"", "", "", "Sinistral Chop", "X-Bluster", ""}, {"", "", "", "Mountainmaker", "", ""}, {"", "", "", "Icy Death", "", ""}, {"", "", "", "Mountainmaker", "Hellion Eye", ""}, {"", "", "", "Feral Death", "", ""}, {"", "", "", "Dark Spawn", "Veinroot's Pulse", ""}, {"", "", "", "Thrashingale", "", ""}, {"", "", "", "Thrashingale Redux", "", ""}, {"", "", "", "Thrashingale Redux", "", ""}, {"", "", "", "Machina Formation", "Deathdealer", ""}, {"", "", "", "Machina Formation", "Deathdealer", ""}, {"", "", "", "Machina Fist", "", ""}, {"", "", "", "Machina Fist", "", ""}, {"", "Stun Grenade", "", "", "", ""}, {"", "Stun Grenade", "", "", "", ""}, {"", "", "", "Arma Cannon", "", ""}, {"", "", "", "Arma Cannon", "Hurricane Punch", ""}, {"", "", "", "Arma Cannon", "Hurricane Punch", ""}, {"", "", "", "Burning Harken", "", ""}, {"", "", "", "Burning Harken", "", ""}, {"", "", "", "Plasma Cannon", "", ""}, {"", "", "", "Plasma Cannon", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Heavenlapse", "Fellstar Gleam", ""}, {"", "", "", "Heavenlapse", "Shadowflame Engine", ""}, {"", "", "", "Paralysis Quills", "Lunchtime", ""}, {"", "", "", "Ovulate", "", ""}, {"", "", "", "Wingflail", "", ""}, {"", "", "", "Wingflail", "Trample", ""}, {"", "", "", "", "", ""}, {"", "", "", "Molten Lunge", "", ""}, {"", "", "", "Heathen Melody", "Feral Proof", ""}, {"", "", "", "Machina Sweep", "", ""}, {"", "", "", "Crimson Catharsis", "", ""}, {"", "Illusory Chaos", "Cast Away Your Carnal Robes", "", "", ""}, {"", "", "", "Hellfire", "Seraphim Soul", ""}, {"", "", "", "Death by Stomping", "", ""}, {"", "", "", "Berserk Tech", "", ""}, {"", "", "", "Berserk Tech", "Hyperslaught Mode (4 enemies)", "Hyperslaught Mode (3 enemies)"}, {"", "Magnus of Life", "Cast Away Your Carnal Robes", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Fryercracker", "", ""}, {"", "", "", "", "", ""}, {"", "", "", "Arma Cannon", "Hurricane Punch", ""}}

    ReadOnly attack_data(,) As Double = {{1, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {2, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1, 45, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {2, 10, 32, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40, 24, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3}, {2, 25, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {2, 20, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {1, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {2, 30, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {7, 15, 13, 11, 9, 7.5, 5, 45, 0, 0, 0, 0, 0, 0, 30, 20, 20, 10, 5, 5, 0, 0, 0, 0, 0, 0, 0, 2}, {6, 40, 30, 20, 20, 20, 150, 0, 0, 0, 0, 0, 0, 0, 150, 50, 30, 20, 20, 10, 0, 0, 0, 0, 0, 0, 0, 4} _
            , {2, 7, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 13, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 18, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 10, 10, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 15, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1, 12.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 22.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 6, 8, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 10, 17.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {2, 10, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {4, 5, 5, 5, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 6, 17.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {3, 6, 8, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 8, 10, 17.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {4, 10, 10, 15, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12.5, 12.5, 17.5, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {4, 10, 10, 15, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12.5, 12.5, 17.5, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {4, 5, 8, 10, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7.5, 10, 12.5, 17.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 7.5, 12.5, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 15, 17.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 15, 15, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 25, 60, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {4, 15, 5, 5, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 10, 10, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {2, 15, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 22.5, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 8, 8, 29, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 10, 36, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {6, 5, 5, 10, 10, 13, 20, 0, 0, 0, 0, 0, 0, 0, 10, 10, 10, 10, 15, 30, 0, 0, 0, 0, 0, 0, 0, 3}, {1, 35, 35, 35, 35, 35, 0, 0, 0, 0, 0, 0, 0, 0, 40, 40, 40, 40, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {1, 37.5, 37.5, 37.5, 37.5, 37.5, 0, 0, 0, 0, 0, 0, 0, 0, 35, 35, 35, 35, 35, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {1, 33, 33, 33, 33, 33, 0, 0, 0, 0, 0, 0, 0, 0, 45, 45, 45, 45, 45, 0, 0, 0, 0, 0, 0, 0, 0, 3}, {2, 20, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 45, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {3, 15, 17, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 20, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3}, {3, 20, 25, 35, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 30, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {3, 18, 22, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 40, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3} _
            , {1, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6}, {1, 55, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {1, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {1, 45, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3}, {1, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 30, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4}, {1, 42, 42, 42, 42, 42, 0, 0, 0, 0, 0, 0, 0, 0, 21, 21, 21, 21, 21, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {2, 30, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 15, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {1, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3}, {1, 75, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 37.5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4}, {1, 75, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5}, {9, 30, 40, 30, 40, 30, 40, 30, 30, 40, 0, 0, 0, 0, 15, 20, 15, 20, 15, 20, 15, 20, 20, 0, 0, 0, 0, 6}, {1, 55, 55, 55, 55, 55, 0, 0, 0, 0, 0, 0, 0, 0, 25, 25, 25, 25, 25, 0, 0, 0, 0, 0, 0, 0, 0, 1}, {1, 55, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 25, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2}, {1, 52, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3}, {6, 20, 10, 10, 10, 20, 50, 0, 0, 0, 0, 0, 0, 0, 30, 15, 10, 5, 5, 5, 0, 0, 0, 0, 0, 0, 0, 4}, {4, 20, 20, 30, 40, 0, 0, 0, 0, 0, 0, 0, 0, 0, 35, 15, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5}, {13, 60, 53, 60, 68, 60, 53, 60, 68, 60, 53, 60, 60, 68, 30, 23, 30, 30, 30, 30, 30, 30, 30, 23, 30, 30, 30, 2}}
    'attack data (0-27): number of hits, offense (1-13), crush (14-26), element

    Public ReadOnly element_name() As String = {"Physical", "Fire", "Ice", "Lightning", "Light", "Darkness"}
    Public ReadOnly element_color() As Color = {Color.FromArgb(&H90, &HA8, &H5A, &H2A), Color.FromArgb(&H90, &HFF, &H20, &H0), Color.FromArgb(&H90, &H0, &HB0, &HFF), Color.FromArgb(&H90, &H0, &HB0, &H0), Color.FromArgb(&H90, &HC0, &HC0, &HC0), Color.FromArgb(&H90, &HC0, &H0, &HFF)}
    ReadOnly element_compatibility(,) As Double = {{1, 0.9, 0.9, 0.9, 0.9, 0.9, 1}, {0.9, 1, 0.5, 0.75, 0.75, 0.75, 1}, {0.9, 0.5, 1, 0.75, 0.75, 0.75, 1}, {0.9, 0.75, 0.75, 1, 0.75, 0.75, 1}, {0.9, 0.75, 0.75, 0.75, 1, 0.5, 1}, {0.9, 0.75, 0.75, 0.75, 0.5, 1, 1}}

    ReadOnly status_name = {"Normal", "Flames", "Frozen", "Shock", "Blind"}
    ReadOnly status_crit(,) As Boolean = {
              {0, 0, 0, 0, 0, 0} _      'Normal
            , {0, 0, 1, 0, 0, 0} _      'Flames
            , {0, 1, 0, 0, 0, 0} _      'Frozen
            , {0, 0, 0, 0, 0, 0} _      'Shock
            , {0, 0, 0, 0, 1, 0}}       'Blind

    ReadOnly EX_combo_name() As String = {"", "Scension Flurry", "Anti-Scension Flurry", "Scension Blitz", "True Scension Blitz", "Anti-Scension Blitz", "Scension Onslaught", "True Scension Onslaught", "Anti-Scension Onslaught", "Fire-Forged Transcension", "Heaven-Forged Transcension", "True Transcension", "Fire-Forged Ascension", "Heaven-Forged Ascension", "True Ascension", "Blessed Ascension", "Sunderbolt", "Heavenflame's Tongue", "Crimson Skies", "True Crimson Skies", "Crimson Sky Tooth", "True Crimson Sky Tooth", "Flame Ice Flurry", "True Flame Ice Flurry", "Heavenly Bloom", "True Heavenly Bloom", "Heaven's Glacier", "True Heaven's Glacier", "True Smoldering Pike", "Smoldering Pike", "Icy Hellpike", "True Icy Hellpike", "Icegleam Shield", "Icegleam Assault", "Rime Flower", "Rime Flower Guardian", "True Rime Flower", "Rime Gleam Armor", "Godspeed the Ice", "Healing Wings", "The Apotheosis", "Reverse Knight", "Reverse Tail", "Trail Rush", "Horse Prance", "Capricorn Header", "Moon Crash", "Paralysis Bell", "Empyreal Thunder", "Dancing Doll", "Dancing Drop", "Dancing Condor", "Arabesque Dance", "Arabesque Doll", "Arabesque Thunder", "Secret Queen", "Secret Queen II", "Swallow's Flight", "Stardust", "Emerald Guard", "Starbreaker", "Thunder Seriatim", "Thunder Seriatim II", "Lightning Turkey", "Lightning Turkey II", "Double Frost", "Double Frost II", "Burning Frost", "Burning Frost II", "Raging Firewheel", "Wheel of Fire and Ice", "Circuit of Fire and Ice", "Goddrake's Thunderclap", "Goddrake's Thunderblast", "Cursedealer's Revels", "Cursedealer's Bacchanal", "Divine Ward", "Spirit Ward", "Spirit Enchantment", "Fellstar Quiver", "Fellstar Trebuchet", "Yin and Yang", "Yin Yang Integration", "Black Yang", "Blackest Yang", "White Yin", "Whitest Yin", "Iceblast", "Ice Queen", "Glacial Queen", "Subzero Parade", "Subzero Festival", "Fiery Ice Queen", "Blazing Glacial Queen", "Frigid Queen's Parade", "Frigid Queen's Festival", "Levingod's Quiver I", "Levingod's Quiver II", "Two-Palmed Defense", "Two Palmed Attack", "Celestial Catastrophe", "Guardian Comet"}
    ReadOnly EX_combo_data(,) As Integer = {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {4, 4, 2, 3, 11, 0, 0, 0, 10, 0, 0}, {4, 1, 2, 6, 11, 0, 0, 0, 10, 0, 0}, {2, 11, 14, 0, 0, 0, 0, 0, 30, 0, 0}, {5, 4, 2, 3, 11, 14, 0, 0, 50, 0, 0}, {5, 1, 2, 6, 11, 14, 0, 0, 50, 0, 0}, {3, 11, 14, 17, 0, 0, 0, 0, 70, 0, 0}, {6, 4, 2, 3, 11, 14, 17, 0, 100, 0, 0}, {6, 1, 2, 6, 11, 14, 17, 0, 100, 0, 0}, {4, 4, 2, 6, 17, 0, 0, 0, 40, 20, 1}, {4, 4, 2, 6, 17, 0, 0, 0, 20, 0, 4}, {4, 4, 2, 6, 17, 0, 0, 0, 30, 0, 0}, {4, 1, 5, 6, 14, 0, 0, 0, 30, 20, 1}, {4, 1, 5, 6, 14, 0, 0, 0, 10, 0, 4}, {4, 1, 5, 6, 14, 0, 0, 0, 20, 0, 0}, {2, 1, 14, 0, 0, 0, 0, 0, 10, 0, 4}, {4, 4, 5, 3, 13, 0, 0, 0, 5, 15, 3}, {4, 1, 2, 6, 12, 0, 0, 0, 15, 0, 1}, {2, 12, 15, 0, 0, 0, 0, 0, 40, 0, 0}, {5, 1, 2, 6, 12, 15, 0, 0, 60, 0, 0}, {3, 12, 15, 18, 0, 0, 0, 0, 100, 0, 0}, {6, 1, 2, 6, 12, 15, 18, 0, 120, 0, 0}, {3, 12, 15, 19, 0, 0, 0, 0, 90, 20, 0}, {6, 1, 2, 6, 12, 15, 19, 0, 110, 20, 0}, {2, 12, 16, 0, 0, 0, 0, 0, 35, 0, 0}, {5, 1, 2, 6, 12, 16, 0, 0, 55, 10, 0}, {3, 12, 16, 19, 0, 0, 0, 0, 80, 30, 0}, {6, 1, 2, 6, 12, 16, 19, 0, 100, 40, 0}, {4, 4, 5, 6, 15, 0, 0, 0, 40, 0, 1}, {4, 4, 5, 6, 15, 0, 0, 0, 35, 0, 0}, {2, 15, 19, 0, 0, 0, 0, 0, 50, 20, 0}, {5, 4, 5, 6, 15, 19, 0, 0, 60, 30, 0}, {3, 4, 3, 16, 0, 0, 0, 0, 10, 0, 0}, {4, 4, 5, 3, 16, 0, 0, 0, 35, 10, 5}, {2, 16, 19, 0, 0, 0, 0, 0, 40, 0, 0}, {4, 4, 3, 16, 19, 0, 0, 0, 20, 0, 0}, {5, 4, 5, 3, 16, 19, 0, 0, 50, 0, 0}, {3, 4, 6, 19, 0, 0, 0, 0, 10, 0, 2}, {4, 4, 5, 6, 19, 0, 0, 0, 40, 10, 5}, {4, 1, 2, 6, 20, 0, 0, 0, 40, 0, 4}, {7, 1, 2, 3, 11, 14, 17, 20, 150, 50, 0}, {2, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 4, 2, 21, 0, 0, 0, 0, 10, 0, 0}, {2, 8, 5, 0, 0, 0, 0, 0, 0, 0, 0}, {3, 1, 8, 5, 0, 0, 0, 0, 0, 0, 0}, {3, 7, 2, 3, 0, 0, 0, 0, 0, 0, 0}, {4, 1, 7, 2, 3, 0, 0, 0, 0, 0, 0}, {5, 1, 7, 2, 3, 28, 0, 0, 40, 0, 0}, {7, 1, 7, 2, 3, 23, 26, 28, 80, 100, 0}, {4, 1, 8, 5, 10, 0, 0, 0, 0, 0, 0}, {5, 1, 8, 5, 10, 25, 0, 0, 20, 0, 0}, {5, 1, 8, 5, 10, 26, 0, 0, 15, 30, 0}, {5, 1, 8, 5, 10, 24, 0, 0, 20, 0, 0}, {5, 1, 8, 5, 10, 24, 0, 0, 30, 0, 1}, {5, 1, 8, 5, 10, 24, 0, 0, 15, 25, 3}, {1, 10, 0, 0, 0, 0, 0, 0, 10, 0, 7}, {2, 10, 21, 0, 0, 0, 0, 0, 10, 0, 7}, {4, 4, 2, 6, 21, 0, 0, 0, 5, 0, 0}, {4, 4, 2, 6, 23, 0, 0, 0, 5, 40, 0}, {4, 1, 2, 6, 22, 0, 0, 0, 10, 0, 2}, {4, 1, 2, 3, 23, 0, 0, 0, 10, 20, 3}, {2, 23, 26, 0, 0, 0, 0, 0, 20, 40, 0}, {5, 4, 2, 6, 23, 26, 0, 0, 20, 50, 0}, {3, 23, 26, 28, 0, 0, 0, 0, 70, 70, 0}, {6, 4, 2, 6, 23, 26, 28, 0, 80, 80, 0}, {2, 22, 25, 0, 0, 0, 0, 0, 20, 0, 0}, {5, 1, 2, 6, 22, 25, 0, 0, 40, 0, 0}, {3, 22, 25, 27, 0, 0, 0, 0, 80, 40, 0}, {6, 1, 2, 6, 22, 25, 27, 0, 100, 40, 0}, {4, 1, 2, 6, 29, 0, 0, 0, 15, 0, 1}, {2, 29, 34, 0, 0, 0, 0, 0, 35, 0, 0}, {5, 1, 2, 6, 29, 34, 0, 0, 55, 10, 0}, {2, 35, 41, 0, 0, 0, 0, 0, 30, 40, 0}, {5, 1, 5, 3, 35, 41, 0, 0, 40, 40, 0}, {2, 37, 43, 0, 0, 0, 0, 0, 25, 0, 0}, {5, 4, 2, 3, 37, 43, 0, 0, 35, 0, 0}, {4, 1, 2, 6, 32, 0, 0, 0, 10, 0, 6}, {2, 32, 36, 0, 0, 0, 0, 0, 20, 0, 0}, {5, 1, 5, 3, 32, 36, 0, 0, 30, 0, 0}, {3, 32, 36, 42, 0, 0, 0, 0, 60, 0, 0}, {6, 1, 5, 3, 32, 36, 42, 0, 70, 0, 0}, {2, 32, 37, 0, 0, 0, 0, 0, 35, 0, 0}, {5, 4, 2, 3, 32, 37, 0, 0, 55, 0, 0}, {3, 32, 37, 43, 0, 0, 0, 0, 65, 30, 0}, {6, 4, 2, 3, 32, 37, 43, 0, 100, 40, 0}, {3, 32, 36, 43, 0, 0, 0, 0, 70, 30, 0}, {6, 1, 5, 3, 32, 36, 43, 0, 100, 40, 0}, {4, 4, 5, 6, 30, 0, 0, 0, 10, 0, 2}, {2, 30, 33, 0, 0, 0, 0, 0, 30, 0, 0}, {5, 4, 5, 6, 30, 33, 0, 0, 40, 0, 0}, {3, 30, 33, 40, 0, 0, 0, 0, 60, 0, 0}, {6, 4, 5, 6, 30, 33, 40, 0, 90, 0, 0}, {3, 30, 33, 39, 0, 0, 0, 0, 50, 20, 0}, {6, 4, 5, 6, 30, 33, 39, 0, 100, 20, 0}, {4, 30, 33, 40, 44, 0, 0, 0, 55, 30, 0}, {7, 4, 5, 6, 30, 33, 40, 44, 110, 40, 0}, {4, 1, 5, 3, 31, 0, 0, 0, 10, 15, 3}, {4, 1, 5, 3, 31, 0, 0, 0, 20, 0, 5}, {4, 1, 5, 3, 34, 0, 0, 0, 10, 0, 2}, {4, 1, 5, 3, 34, 0, 0, 0, 30, 0, 5}, {4, 1, 5, 3, 38, 0, 0, 0, 30, 20, 0}, {4, 1, 2, 3, 44, 0, 0, 0, 40, 0, 6}}
    'EX combo data (0-10): length, cards (1-7), offense bonus, crush bonus, requirement
    'EX combo requirements (0-7): none, fire weapon, ice weapon, lightning weapon, holy weapon, armor, accessory, enemy down

    ReadOnly party_HP(,) As Integer = {{0, 100, 150, 235, 290, 350, 400, 445, 495, 545, 590, 630, 675, 715, 760, 800, 840, 880, 920, 960, 1000, 1055, 1105, 1160, 1210, 1265, 1320, 1370, 1425, 1480, 1530, 1590, 1655, 1715, 1780, 1840, 1900, 1965, 2025, 2090, 2150, 2215, 2280, 2345, 2410, 2475, 2540, 2605, 2670, 2735, 2800, 4200}, {0, 100, 130, 180, 230, 280, 325, 370, 415, 455, 500, 540, 575, 615, 650, 690, 730, 765, 805, 840, 880, 930, 975, 1020, 1070, 1115, 1160, 1210, 1255, 1303, 1350, 1407, 1464, 1521, 1578, 1635, 1692, 1749, 1806, 1863, 1920, 1978, 2036, 2094, 2152, 2210, 2268, 2326, 2384, 2442, 2500, 3750}, {0, 250, 250, 250, 260, 270, 315, 355, 400, 440, 480, 520, 555, 595, 630, 670, 705, 745, 780, 820, 855, 899, 944, 988, 1033, 1077, 1122, 1166, 1211, 1255, 1300, 1350, 1400, 1450, 1500, 1545, 1595, 1640, 1690, 1740, 1790, 1840, 1890, 1945, 1995, 2045, 2095, 2150, 2200, 2250, 2300, 3450}}
    ReadOnly party_knockdown(,) As Integer = {{0, 40, 68, 106, 131, 158, 180, 201, 223, 246, 266, 284, 304, 322, 342, 360, 378, 396, 414, 432, 450, 475, 498, 522, 545, 570, 594, 617, 642, 666, 689, 716, 745, 772, 801, 828, 855, 885, 912, 941, 968, 997, 1026, 1056, 1085, 1114, 1143, 1173, 1202, 1231, 1260, 1890}, {0, 44, 74, 117, 144, 173, 198, 221, 245, 270, 292, 312, 334, 354, 376, 396, 416, 436, 455, 475, 495, 523, 547, 574, 599, 626, 653, 678, 706, 733, 757, 787, 820, 849, 881, 911, 941, 973, 1003, 1035, 1064, 1097, 1129, 1161, 1193, 1225, 1257, 1290, 1322, 1354, 1386, 2079}, {0, 100, 100, 100, 117, 142, 162, 180, 201, 221, 239, 255, 274, 290, 308, 324, 340, 356, 373, 389, 405, 428, 448, 470, 490, 513, 535, 555, 577, 599, 620, 644, 671, 695, 721, 745, 770, 796, 820, 846, 871, 897, 923, 950, 976, 1003, 1029, 1055, 1081, 1108, 1134, 1701}}
    ReadOnly party_knockout(,) As Integer = {{0, 60, 101, 159, 196, 236, 270, 301, 335, 368, 398, 425, 456, 483, 513, 540, 567, 594, 621, 648, 675, 713, 746, 783, 817, 854, 891, 925, 962, 999, 1033, 1073, 1118, 1158, 1202, 1242, 1283, 1327, 1367, 1411, 1451, 1496, 1539, 1583, 1627, 1671, 1715, 1759, 1802, 1847, 1890, 2835}, {0, 64, 108, 170, 209, 252, 288, 321, 357, 393, 425, 454, 486, 515, 547, 576, 605, 634, 662, 691, 720, 760, 796, 835, 871, 911, 950, 986, 1026, 1066, 1102, 1145, 1192, 1235, 1282, 1325, 1368, 1415, 1458, 1505, 1548, 1595, 1642, 1689, 1735, 1782, 1829, 1876, 1922, 1970, 2016, 3024}, {0, 120, 120, 120, 183, 221, 252, 281, 312, 344, 372, 397, 426, 451, 479, 504, 529, 554, 580, 605, 630, 665, 697, 731, 762, 797, 832, 863, 898, 932, 964, 1002, 1043, 1081, 1121, 1159, 1197, 1238, 1276, 1317, 1355, 1396, 1436, 1478, 1518, 1560, 1600, 1642, 1682, 1723, 1764, 2646}}
    ReadOnly party_crush_limit(,) As Integer = {{0, 80, 135, 212, 261, 315, 360, 401, 446, 491, 531, 567, 608, 644, 684, 720, 756, 792, 828, 864, 900, 950, 995, 1044, 1089, 1139, 1188, 1233, 1283, 1332, 1377, 1431, 1490, 1544, 1602, 1656, 1710, 1769, 1823, 1881, 1935, 1994, 2052, 2111, 2169, 2228, 2286, 2345, 2403, 2462, 2520, 3780}, {0, 80, 135, 212, 261, 315, 360, 401, 446, 491, 531, 567, 608, 644, 684, 720, 756, 792, 828, 864, 900, 950, 995, 1044, 1089, 1139, 1188, 1233, 1283, 1332, 1377, 1431, 1490, 1544, 1602, 1656, 1710, 1769, 1823, 1881, 1935, 1994, 2052, 2111, 2169, 2228, 2286, 2345, 2403, 2462, 2520, 3780}, {0, 212, 212, 212, 261, 315, 360, 401, 446, 491, 531, 567, 608, 644, 684, 720, 756, 792, 828, 864, 900, 950, 995, 1044, 1089, 1139, 1188, 1233, 1283, 1332, 1377, 1431, 1490, 1544, 1602, 1656, 1710, 1769, 1823, 1881, 1935, 1994, 2052, 2111, 2169, 2228, 2286, 2345, 2403, 2462, 2520, 3780}}
    ReadOnly party_defense(,,) As Integer = {{{0, 0, 0, 0, 0, 0}, {7, 8, 4, 5, 6, 4}, {8, 10, 5, 6, 7, 5}, {10, 11, 5, 7, 8, 5}, {11, 12, 6, 8, 9, 6}, {12, 14, 7, 9, 10, 7}, {13, 15, 8, 10, 12, 8}, {15, 17, 8, 11, 13, 8}, {16, 18, 9, 11, 14, 9}, {17, 20, 10, 12, 15, 10}, {19, 21, 11, 13, 16, 11}, {20, 23, 11, 14, 17, 11}, {21, 24, 12, 15, 18, 12}, {22, 26, 13, 16, 19, 13}, {24, 27, 14, 17, 20, 14}, {25, 29, 14, 18, 21, 14}, {26, 30, 15, 19, 23, 15}, {28, 32, 16, 20, 24, 16}, {29, 33, 16, 21, 25, 16}, {30, 34, 17, 22, 26, 17}, {31, 36, 18, 22, 27, 18}, {33, 37, 19, 23, 28, 19}, {34, 39, 19, 24, 29, 19}, {35, 40, 20, 25, 30, 20}, {37, 42, 21, 26, 31, 21}, {38, 43, 22, 27, 32, 22}, {39, 45, 22, 28, 34, 22}, {40, 46, 23, 29, 35, 23}, {42, 48, 24, 30, 36, 24}, {43, 49, 25, 31, 37, 25}, {44, 51, 25, 32, 38, 25}, {46, 52, 26, 33, 39, 26}, {47, 54, 27, 34, 40, 27}, {48, 55, 28, 34, 41, 28}, {50, 57, 28, 35, 42, 28}, {51, 58, 29, 36, 44, 29}, {52, 60, 30, 37, 45, 30}, {53, 61, 31, 38, 46, 31}, {55, 63, 31, 39, 47, 31}, {56, 64, 32, 40, 48, 32}, {57, 66, 33, 41, 49, 33}, {59, 67, 34, 42, 50, 34}, {60, 68, 34, 43, 51, 34}, {61, 70, 35, 44, 52, 35}, {62, 71, 36, 45, 54, 36}, {64, 73, 36, 46, 55, 36}, {65, 74, 37, 46, 56, 37}, {66, 76, 38, 47, 57, 38}, {67, 77, 39, 48, 58, 39}, {69, 79, 39, 49, 59, 39}, {70, 80, 40, 50, 60, 40}, {140, 160, 80, 100, 120, 80}}, {{0, 0, 0, 0, 0, 0}, {8, 3, 3, 8, 5, 4}, {10, 4, 4, 10, 6, 5}, {11, 4, 4, 11, 7, 5}, {12, 5, 5, 12, 8, 6}, {14, 5, 5, 14, 9, 7}, {15, 6, 6, 15, 10, 8}, {17, 6, 6, 17, 11, 8}, {18, 7, 7, 18, 11, 9}, {20, 7, 7, 20, 12, 10}, {21, 8, 8, 21, 13, 11}, {23, 9, 9, 23, 14, 11}, {24, 9, 9, 24, 15, 12}, {26, 10, 10, 26, 16, 13}, {27, 10, 10, 27, 17, 14}, {29, 11, 11, 29, 18, 14}, {30, 11, 11, 30, 19, 15}, {32, 12, 12, 32, 20, 16}, {33, 12, 12, 33, 21, 16}, {34, 13, 13, 34, 22, 17}, {36, 13, 13, 36, 22, 18}, {37, 14, 14, 37, 23, 19}, {39, 15, 15, 39, 24, 19}, {40, 15, 15, 40, 25, 20}, {42, 16, 16, 42, 26, 21}, {43, 16, 16, 43, 27, 22}, {45, 17, 17, 45, 28, 22}, {46, 17, 17, 46, 29, 23}, {48, 18, 18, 48, 30, 24}, {49, 18, 18, 49, 31, 25}, {51, 19, 19, 51, 32, 25}, {52, 20, 20, 52, 33, 26}, {54, 20, 20, 54, 34, 27}, {55, 21, 21, 55, 34, 28}, {57, 21, 21, 57, 35, 28}, {58, 22, 22, 58, 36, 29}, {60, 22, 22, 60, 37, 30}, {61, 23, 23, 61, 38, 31}, {63, 23, 23, 63, 39, 31}, {64, 24, 24, 64, 40, 32}, {66, 25, 25, 66, 41, 33}, {67, 25, 25, 67, 42, 34}, {68, 26, 26, 68, 43, 34}, {70, 26, 26, 70, 44, 35}, {71, 27, 27, 71, 45, 36}, {73, 27, 27, 73, 46, 36}, {74, 28, 28, 74, 46, 37}, {76, 28, 28, 76, 47, 38}, {77, 29, 29, 77, 48, 39}, {79, 29, 29, 79, 49, 39}, {80, 30, 30, 80, 50, 40}, {160, 60, 60, 160, 100, 80}}, {{0, 0, 0, 0, 0, 0}, {7, 8, 11, 8, 10, 11}, {7, 8, 11, 8, 10, 11}, {7, 8, 11, 8, 10, 11}, {7, 9, 12, 9, 11, 12}, {7, 10, 14, 10, 12, 14}, {8, 12, 15, 12, 13, 15}, {8, 13, 17, 13, 15, 17}, {9, 14, 18, 14, 16, 18}, {10, 15, 20, 15, 17, 20}, {11, 16, 21, 16, 19, 21}, {11, 17, 23, 17, 20, 23}, {12, 18, 24, 18, 21, 24}, {13, 19, 26, 19, 22, 26}, {14, 20, 27, 20, 24, 27}, {14, 21, 29, 21, 25, 29}, {15, 23, 30, 23, 26, 30}, {16, 24, 32, 24, 28, 32}, {16, 25, 33, 25, 29, 33}, {17, 26, 34, 26, 30, 34}, {18, 27, 36, 27, 31, 36}, {19, 28, 37, 28, 33, 37}, {19, 29, 39, 29, 34, 39}, {20, 30, 40, 30, 35, 40}, {21, 31, 42, 31, 37, 42}, {22, 32, 43, 32, 38, 43}, {22, 34, 45, 34, 39, 45}, {23, 35, 46, 35, 40, 46}, {24, 36, 48, 36, 42, 48}, {25, 37, 49, 37, 43, 49}, {25, 38, 51, 38, 44, 51}, {26, 39, 52, 39, 46, 52}, {27, 40, 54, 40, 47, 54}, {28, 41, 55, 41, 48, 55}, {28, 42, 57, 42, 50, 57}, {29, 44, 58, 44, 51, 58}, {30, 45, 60, 45, 52, 60}, {31, 46, 61, 46, 53, 61}, {31, 47, 63, 47, 55, 63}, {32, 48, 64, 48, 56, 64}, {33, 49, 66, 49, 57, 66}, {34, 50, 67, 50, 59, 67}, {34, 51, 68, 51, 60, 68}, {35, 52, 70, 52, 61, 70}, {36, 54, 71, 54, 62, 71}, {36, 55, 73, 55, 64, 73}, {37, 56, 74, 56, 65, 74}, {38, 57, 76, 57, 66, 76}, {39, 58, 77, 58, 67, 77}, {39, 59, 79, 59, 69, 79}, {40, 60, 80, 60, 70, 80}, {80, 120, 160, 120, 140, 160}}}

    Public ReadOnly QM_name() As String = {"Blank Magnus", "Pristine Water", "Drinking Water", "Stale Water", "Fireglow Stone", "Blaze", "Flame", "Mother Sunshine", "Lightning Shroom", "Jolt Shroom", "Spark Shroom", "Treasure Lowdown", "Mother-in-Law's Secret", "Yesterday's News", "Stone", "Boulder", "Pow Milk", "Pow Milk Yogurt", "Pow Milk Cheese", "Glubberfish Filet", "Rotten Food", "Celestial Flower Seed", "Celestial Flower Bud", "Celestial Flower", "Cloud", "Salty Water", "Salt", "Lava", "Hot Rock", "Mountain Apple", "Mountain Apple Vinegar", "Mountain Apple Wine", "Magna Mess-Up", "Chronic Fatigue", "Rotting Mountain Apple", "Fruit Fit for an Emperor", "Meat Fit for an Emperor", "Well-Done Meat", "Juwar's Testimony", "Juwar's Testimony", "Juwar's Testimony", "Juwar's Testimony", "Nollin's Testimony", "Nollin's Testimony", "Mallo's Testimony", "Mallo's Testimony", "Bein's Testimony", "Rock Salt", "Diadem Cloud", "Pac-Man", "Pac-Land", "Pac-Mania", "Chunk of Rubber", "Hero's Pickax", "Lava Lord's Skull", "Sandfeeder Silk", "Sparkling Snow", "Nameless Flower", "Pressed Flower", "Bloodstained Crest", "Diadem Royal Crest", "Egg", "Boiled Egg", "Machina Oil", "Gold Beetle Carapace", "Gold Nugget", "Adventure Novel", "Naughty Novel", "Dense Medical Tome", "Read-to-Death Book", "Chef-Prepared Meat", "Empty Book", "Pow Meat", "Shaved Ice of Love", "Portrait of Verus", "Extreme Stress", "Mound of Soot", "Soot Soup", "Withered Branch", "Pretty Stone", "Plain Old Shaved Ice", "Dark Powder", "Travel Log", "Basic Medical Primer", "Heartflask", "Heartflask", "Heartflask", "Heartflask", "Heartflask", "Heartflask", "Heartflask", "Heartflask", "Paramours' Secret", "Stinging Antiseptic", "Cottoncap Gauze", "Fluella Cooties", "Warm Cheers", "Icy Jeers", "Real-Deal Mask", "Thornflower", "Thornflower Nectar", "Fluffy Pillow", "Sandcap Spores", "Love Syrup", "Sandcaps", "Machina Gas", "Yesterbean Variant", "Poor Excuse for a Joke", "Phantasmagoria", "Cottoncap Fruit", "Machina Communicator", "Heartenbrace", "Immigration Papers", "Holoholobird's Plume", "Celestial Fell-Branch", "Fire Dagroot", "Ice Dagroot", "Lightning Dagroot", "Dark Dagroot", "Holy Dagroot", "Black Dragon's Horn", "Balmsand", "Goopy Machina Oil", "Foul Air", "Fresh Air", "Power Pellet", "Light Powder", "Traditional KM Cookie", "Original KM Cookie", "Lotus Leaf", "Photosynth Lily", "Freezing Rain", "Oleflour", "Gust Boulder", "Yesterbean", "Medic Kit", "Gena's Pinion", "Toy Sword", "Dull Times", "Gold Beetle Wallet", "Hero License", "Magnetite Waves", "Good Times", "Eau de Mouche", "Holy Droplet", "Terrible Song", "Sweet Song", "Heartbreaking Song", "Flame Ice", "Coliseum Dog Tags", "Metal Device", "Bomb Detonator", "Rockfly Corpse", "Royal Mirror", "Sagi Wanted Poster", "Milly Wanted Poster", "Guillo Wanted Poster", "Local Hero Sagi", "Local Hero Milly", "Local Hero Guillo", "Woodfellah", "Billowsmoke", "Holoholo Fruit", "Holoflower Nectar", "Half-Baked Greythorne", "Greythorne Storybook", "Greythorne's Song", "Tub-Time Greythorne", "Floppy Greythorne", "Fluffy Greythorne", "Spring-Lord's Voice", "Rainbow", "Primordial Cactus", "Ancient Mask", "Stewed Mud Potatoes", "Fire Moss", "Potted Celestial Flower", "Potted Heartenbrace", "Potted Nameless Flower", "Flowerpot", "Celestial Tree"}
    Public ReadOnly QM_effect() As Integer = {0, 3, 3, 3, 9, 4, 4, 4, 38, 11, 11, 31, 31, 31, 14, 14, 33, 33, 33, 32, 35, 16, 16, 16, 7, 12, 12, 36, 43, 35, 12, 34, 0, 35, 44, 35, 27, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 7, 0, 0, 48, 5, 23, 0, 0, 37, 47, 0, 0, 0, 34, 35, 25, 24, 0, 23, 32, 25, 8, 15, 0, 27, 21, 28, 20, 6, 6, 0, 6, 3, 42, 0, 25, 29, 29, 29, 29, 29, 29, 29, 29, 31, 7, 47, 16, 46, 46, 27, 8, 8, 45, 5, 33, 5, 35, 25, 10, 47, 45, 0, 29, 0, 0, 16, 9, 10, 11, 13, 12, 0, 19, 25, 18, 48, 34, 41, 15, 27, 0, 0, 10, 0, 14, 25, 46, 30, 15, 0, 24, 22, 5, 0, 0, 12, 0, 0, 0, 37, 0, 0, 0, 33, 12, 32, 33, 34, 32, 33, 34, 0, 45, 35, 6, 23, 1, 2, 22, 23, 23, 48, 48, 8, 13, 34, 9, 16, 29, 47, 0, 23, 0}
    Public ReadOnly QM_bonus() As Integer = {0, 3, 2, 1, 5, 2, 1, 3, 3, 2, 1, 5, 1, -5, 1, 5, 3, 5, 10, 5, -5, 3, 5, 8, 3, 1, 3, 3, 1, 5, 10, 5, 0, -3, 10, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 8, 0, 0, 100, 5, 3, 0, 0, 5, 3, 0, 0, 0, 3, 7, -5, 5, 0, 5, 10, 20, -10, 8, 0, 3, -20, 100, -15, -3, -5, 0, 10, 10, 5, 0, 5, 0, 10, 30, 50, 100, 10, 30, 50, 1, 10, 5, -20, 10, -30, 5, 5, 8, 10, 3, 5, 5, -5, 3, 10, -10, 3, 0, 3, 0, 0, 3, 5, 5, 5, 5, 5, 0, 3, -10, 20, 5, 3, 5, 2, 2, 0, 0, 5, 0, 3, 5, 10, 1, 3, 0, 10, 1, 5, 0, 0, 15, 0, 0, 0, 3, 0, 0, 0, -10, 10, -8, -8, -8, 12, 12, 12, 0, 5, 3, 5, 3, 20, -20, -1, 5, 8, 3, 5, 10, 8, 5, 3, 8, 3, 3, 0, 10, 0}

    ReadOnly aura_name() As String = {"No aura", "Attack", "Crush", "Guard", "Life", "Speed", "Earth", "Fire", "Ice", "Thunder", "Dark", "Light"}
    ReadOnly aura_data(,) As Integer = {{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 6, 10, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 6, 25, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 6, 50, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 6, 0, 15, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 6, 0, 30, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 6, 0, 75, 0, 0, 0, 0, 0, 0}, {0, 20, 10, 0, 0, 0, 5, 5, 5, 5, 5, 5}, {0, 40, 20, 0, 0, 0, 10, 10, 10, 10, 10, 10}, {0, 100, 50, 0, 0, 0, 25, 25, 25, 25, 25, 25}, {50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {100, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {200, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 0, 15, 15, 8, 0, 0, -2, 0, 0}, {0, 0, 0, 0, 30, 30, 15, 0, 0, -4, 0, 0}, {0, 0, 0, 0, 75, 75, 40, 0, 0, -10, 0, 0}, {0, 0, 0, 1, 15, 15, 0, 8, -2, 0, 0, 0}, {0, 0, 0, 1, 30, 30, 0, 15, -4, 0, 0, 0}, {0, 0, 0, 1, 75, 75, 0, 40, -10, 0, 0, 0}, {0, 0, 0, 2, 15, 15, 0, -2, 8, 0, 0, 0}, {0, 0, 0, 2, 30, 30, 0, -4, 15, 0, 0, 0}, {0, 0, 0, 2, 75, 75, 0, -10, 40, 0, 0, 0}, {0, 0, 0, 3, 15, 15, -2, 0, 0, 8, 0, 0}, {0, 0, 0, 3, 30, 30, -4, 0, 0, 15, 0, 0}, {0, 0, 0, 3, 75, 75, -10, 0, 0, 40, 0, 0}, {0, 0, 0, 5, 15, 15, 0, 0, 0, 0, -2, 8}, {0, 0, 0, 5, 30, 30, 0, 0, 0, 0, -4, 15}, {0, 0, 0, 5, 75, 75, 0, 0, 0, 0, -10, 40}, {0, 0, 0, 4, 15, 15, 0, 0, 0, 0, 8, -2}, {0, 0, 0, 4, 30, 30, 0, 0, 0, 0, 15, -4}, {0, 0, 0, 4, 75, 75, 0, 0, 0, 0, 40, -10}}

    ReadOnly multi_target_attacks() As Integer = {24, 28, 30, 33, 35, 38, 39, 40, 41, 44}
    ReadOnly airborne_enemies() As Integer = {20, 21, 22, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 47, 48, 49, 51, 52, 69, 70, 71, 72, 73, 74, 75, 82, 83, 84, 85, 86, 87, 88, 89, 90, 95, 96, 122, 131, 132, 133}
    ReadOnly multi_phase_enemies() As Integer = {117, 119, 121, 131, 133, 136, 140}
    ReadOnly final_phase_battles() As Integer = {262, 264, 263, 157, 158, 159, 256}

    ReadOnly boost_weapons() As Integer = {78, 81, 89, 95, 99, 106, 121, 143, 151, 161, 184, 186}
    ReadOnly boost_weapon_element() As Integer = {0, 1, 1, 2, 2, 3, 5, 0, 2, 4, 3, 4}
    ReadOnly boost_weapon_bonus() As Integer = {5, 5, 30, 5, 5, 5, 5, 30, 30, 5, 10, 20}

    ReadOnly flames_weapons() As Integer = {53, 56, 57, 84, 85, 89, 90, 92, 93, 448}
    ReadOnly freeze_weapons() As Integer = {135, 152, 153, 168, 182, 449}
    ReadOnly shock_weapons() As Integer = {110, 136, 138, 159, 450}
    ReadOnly blind_weapons() As Integer = {122, 188, 452}

    ReadOnly english_EX_combos() As Integer = {83, 85, 92, 93, 94, 95}
    ReadOnly english_EX_combo_bonus() As Integer = {80, 80, 80, 110, 85, 150}

    Public ReadOnly default_color As Color = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
    ReadOnly button_row_1_Y_pos As Integer = 14
    ReadOnly button_row_2_Y_pos As Integer = 92
    ReadOnly rows As Integer = 32
    ReadOnly hit_modifier_row() As Integer = {4, 5, 21}
    ReadOnly clickable_rows() As Integer = {4, 5, 7, 8, 10, 16, 21, 27}
    ReadOnly E_hit_modifier_row() As Integer = {5, 6, 14}
    ReadOnly E_clickable_rows() As Integer = {5, 6, 8, 14, 21}

    Public ReadOnly variable() As String = {"Base offense", "Attack offense", "Attack crush", "Attack boost factor", "Offense deviation", "Crush deviation", "Electric Helm factor", "Weapon offense", "Weapon crush", "Element compatibility", "Weapon factor", "Quest magnus bonus", "Aura offense", "Aura crush", "EX combo offense factor", "EX combo crush factor", "Critical hit factor", "Base defense", "Crush limit", "Crush status", "Defense boost factor", "Defense deviation", "Total offense", "Total crush", "Total defense", "Armor", "Multiplier", "Damage output", "Crush output", "Total damage output", "Total crush output", "HP remaining"}
    Public ReadOnly E_variable() As String = {"Base offense", "Base crush", "Attack offense", "Attack crush", "Attack boost factor", "Offense deviation", "Crush deviation", "Armor bonus", "Critical hit factor", "Base defense", "Quest magnus defense", "Crush limit", "Crush status", "Defense boost factor", "Defense deviation", "Vanishing Cloak factor", "Aura defense", "Total offense", "Total crush", "Total defense", "Armor", "Damage output", "Crush output", "Total damage output", "Total crush output", "HP remaining"}

    Public ReadOnly version As String = FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion

    Private Declare Function OpenProcess Lib "kernel32" (dwDesiredAccess As Integer, bInheritHandle As Integer, dwProcessId As Integer) As Integer
    Private Declare Function ReadProcessMemory Lib "kernel32" Alias "ReadProcessMemory" (hProcess As Integer, lpBaseAddress As Int64, ByRef lpBuffer As Integer, nSize As Integer, ByRef lpNumberOfBytesWritten As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (hObject As IntPtr) As Boolean
    Const PROCESS_ALL_ACCESS = &H1F0FF

    Private Sub Open() Handles MyBase.Load
        Hide()
        Text = "Baten Kaitos Origins Damage Calculator"
        Icon = New Icon(Me.GetType(), "icon.ico")
        Application.CurrentCulture = New Globalization.CultureInfo("EN-US")
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        DoubleBuffered = True
        KeyPreview = True
        BackColor = Color.LightGray
        MinimumSize = New Size(857, 531)
        LoadWindowData()

        With My.Settings
            .SagiLevel = Clamp(.SagiLevel, 1, 100)
            .MillyLevel = Clamp(.MillyLevel, 1, 100)
            .GuilloLevel = Clamp(.GuilloLevel, 1, 100)
            .DeckClass = Clamp(.DeckClass, 1, 30)
            .Character = Clamp(.Character, 0, 2)
            .Target = Clamp(.Target, 1, 145)

            If .PartyOrder.Length <> 3 Then
                .PartyOrder = "012"
            Else
                For x = 0 To 2
                    Dim i As Char = x.ToString
                    If .PartyOrder.Count(Function(c As Char) c = i) <> 1 Then
                        .PartyOrder = "012"
                        Exit For
                    End If
                Next
            End If

            If .Auras.Length <> 6 OrElse Not IsHexadecimal(.Auras) Then
                .Auras = "000000"
            End If
            If .QuestMagnus.Length <> 48 OrElse Not IsHexadecimal(.QuestMagnus) Then
                .QuestMagnus = "000000000000000000000000000000000000000000000000"
            End If
            If .ResultsRow.Length <> 32 OrElse Not IsBinary(.ResultsRow) Then
                .ResultsRow = "11111111111111111111111111111111"
            End If
            If .EnemyResultsRow.Length <> 26 OrElse Not IsBinary(.EnemyResultsRow) Then
                .EnemyResultsRow = "11111111111111111111111111"
            End If
            If .Heavenlapse.Length <> 9 OrElse Not IsBinary(.Heavenlapse) Then
                .Heavenlapse = "100100101"
            End If
            If .AphelionDustwake.Length <> 13 OrElse Not IsBinary(.AphelionDustwake) Then
                .AphelionDustwake = "1001010101010"
            End If
            If .Fusillade.Length <> 16 OrElse Not IsBinary(.Fusillade) Then
                .Fusillade = "1010010100110100"
            End If
            If .CrimsonCatharsis.Length <> 10 OrElse Not IsBinary(.CrimsonCatharsis) Then
                .CrimsonCatharsis = "0110001001"
            End If
            If .BerserkTech.Length <> 5 OrElse Not IsBinary(.BerserkTech) Then
                .BerserkTech = "01001"
            End If
            If .HyperslaughtMode4.Length <> 16 OrElse Not IsBinary(.HyperslaughtMode4) Then
                .HyperslaughtMode4 = "0010001110010110"
            End If
            If .HyperslaughtMode3.Length <> 10 OrElse Not IsBinary(.HyperslaughtMode3) Then
                .HyperslaughtMode3 = "0100101100"
            End If
            If .MagnusOfLife.Length <> 10 OrElse Not IsBinary(.MagnusOfLife) Then
                .MagnusOfLife = "1100100100"
            End If
        End With


        ' PARTY

        char_icon(0) = New Bitmap(My.Resources.ResourceManager.GetObject("sagi"), New Size(70, 70))
        char_icon(1) = New Bitmap(My.Resources.ResourceManager.GetObject("milly"), New Size(70, 70))
        char_icon(2) = New Bitmap(My.Resources.ResourceManager.GetObject("guillo"), New Size(70, 70))

        For x = 0 To 2
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 75

            char_image(x) = New PictureBox()
            With char_image(x)
                .Size = New Size(70, 70)
                .Location = New Point(10, 10 + top)
                .Cursor = Cursors.Hand
                .Tag = x + 1
                .Image = char_icon(x)
                AddHandler .MouseClick, AddressOf ChangeFocus
                AddHandler .Click, AddressOf SwitchCharacter
            End With
            Controls.Add(char_image(x))

            level_selector(x) = New ComboBox()
            With level_selector(x)
                .Size = New Size(59, 21)
                .Location = New Point(90, 24 + top)
                .MaxLength = 3
                .Tag = x
                For y = 1 To 100
                    .Items.Add(y)
                Next
                AddHandler .KeyPress, AddressOf FilterInput
                AddHandler .TextChanged, AddressOf CheckLevel
                AddHandler .LostFocus, AddressOf FixLevel
            End With
            Controls.Add(level_selector(x))

            actual_level(x) = New Label()
            With actual_level(x)
                .Hide()
                .Size = New Size(59, 21)
                .Location = New Point(90, 50 + top)
                .TextAlign = ContentAlignment.MiddleLeft
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .Tag = x
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(actual_level(x))

            aura_type(x) = New ComboBox()
            With aura_type(x)
                .Size = New Size(69, 21)
                .Location = New Point(155, 24 + top)
                .Tag = x
                .DropDownStyle = ComboBoxStyle.DropDownList
                For y = 0 To 11
                    .Items.Add(aura_name(y))
                Next
                AddHandler .SelectedIndexChanged, AddressOf ChangeAura
            End With
            Controls.Add(aura_type(x))

            aura_level(x) = New ComboBox()
            With aura_level(x)
                .Hide()
                .Size = New Size(69, 21)
                .Location = New Point(155, 50 + top)
                .Tag = x
                .DropDownStyle = ComboBoxStyle.DropDownList
                For y = 0 To 2
                    .Items.Add("Lv. " & y + 1)
                Next
                AddHandler .SelectedIndexChanged, AddressOf ChangeAuraLevel
            End With
            Controls.Add(aura_level(x))

            equipment(x) = New PictureBox()
            With equipment(x)
                .Hide()
                .Size = New Size(40, 64)
                .Location = New Point(235, 16 + top)
                .Cursor = Cursors.Hand
                .Name = x + 1
                AddHandler .MouseClick, AddressOf ChangeFocus
                AddHandler .Click, AddressOf RemoveEquipment
            End With
            Controls.Add(equipment(x))

            eq_durability(x) = New ComboBox()
            With eq_durability(x)
                .Hide()
                .Size = New Size(48, 21)
                .Location = New Point(282, 37 + top)
                .DropDownStyle = ComboBoxStyle.DropDownList
                .Tag = x + 1
                AddHandler .SelectedIndexChanged, AddressOf Calculate
            End With
            Controls.Add(eq_durability(x))
        Next

        Dim target_x As Integer = 340
        Dim target_y As Integer = 15


        ' ENEMY

        target_image = New PictureBox()
        With target_image
            .Size = New Size(125, 150)
            .Location = New Point(target_x, 14)
            .Cursor = Cursors.Hand
            .Name = 7
            .SizeMode = PictureBoxSizeMode.StretchImage
            AddHandler .Click, AddressOf ButtonClick
        End With
        Controls.Add(target_image)

        final_phase = New CheckBox()
        With final_phase
            .Size = New Size(169, 24)
            .Location = New Point(target_x + 135, target_y + 75)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Final phase"
            AddHandler .CheckedChanged, AddressOf ToggleFinalPhase
        End With
        Controls.Add(final_phase)

        shield = New CheckBox()
        With shield
            .Size = New Size(169, 24)
            .Location = New Point(target_x + 135, target_y + 100)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Shield"
            AddHandler .CheckedChanged, AddressOf ToggleShield
        End With
        Controls.Add(shield)

        secondary_target = New CheckBox()
        With secondary_target
            .Size = New Size(169, 24)
            .Location = New Point(target_x + 135, target_y + 125)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Secondary target"
            AddHandler .CheckedChanged, AddressOf Calculate
        End With
        Controls.Add(secondary_target)

        For x = 0 To 12
            target_data(x) = New Label()
            With target_data(x)
                .BackColor = default_color
                .TextAlign = ContentAlignment.MiddleCenter
                AddHandler .Click, AddressOf ChangeFocus
            End With
        Next

        With target_data(0)                                         'target name
            .Size = New Size(169, 25)
            .Location = New Point(target_x + 135, target_y - 1)
        End With

        With target_data(1)
            .Size = New Size(84, 24)
            .Location = New Point(target_x + 135, target_y + 25)
            .Text = "Knockdown"
        End With

        With target_data(2)                                         'knockdown
            .Size = New Size(84, 24)
            .Location = New Point(target_x + 220, target_y + 25)
        End With

        With target_data(3)
            .Size = New Size(84, 24)
            .Location = New Point(target_x + 135, target_y + 50)
            .Text = "Knockout"
        End With

        With target_data(4)                                         'knockout
            .Size = New Size(84, 24)
            .Location = New Point(target_x + 220, target_y + 50)
        End With

        With target_data(5)
            .Size = New Size(49, 24)
            .Location = New Point(target_x, 175)
            .Text = "HP"
        End With

        With target_data(6)
            .Size = New Size(49, 24)
            .Location = New Point(target_x, 200)
            .Text = "Eff. HP"
        End With

        With target_data(7)                                         'effective HP
            .Size = New Size(54, 24)
            .Location = New Point(target_x + 50, 200)
        End With

        With target_data(8)                                         'true max HP
            .Size = New Size(54, 24)
            .Location = New Point(target_x + 105, 175)
            AddHandler .Click, AddressOf ResetHP
        End With

        With target_data(9)                                         'effective max HP
            .Size = New Size(54, 24)
            .Location = New Point(target_x + 105, 200)
        End With

        With target_data(10)
            .Size = New Size(119, 24)
            .Location = New Point(target_x, 200)
            .Text = "Armor durability"
        End With

        With target_data(11)
            .Size = New Size(169, 24)
            .Location = New Point(target_x + 135, target_y + 75)
            .Hide()
        End With

        With target_data(12)
            .Size = New Size(169, 24)
            .Location = New Point(target_x + 135, target_y + 100)
            .Hide()
        End With

        For x = 0 To 12
            Controls.Add(target_data(x))
        Next

        enemy_HP = New TextBox()
        With enemy_HP
            .AutoSize = False
            .Size = New Size(54, 24)
            .Location = New Point(target_x + 50, 175)
            .MaxLength = 5
            .TextAlign = HorizontalAlignment.Center
            AddHandler .TextChanged, AddressOf CheckHP
            AddHandler .MouseWheel, AddressOf ScrollHP
            AddHandler .KeyPress, AddressOf FilterInput
            AddHandler .LostFocus, AddressOf FixHP
        End With
        Controls.Add(enemy_HP)

        down = New CheckBox()
        With down
            .Size = New Size(74, 24)
            .Location = New Point(target_x + 160, 175)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Down"
            AddHandler .CheckedChanged, AddressOf ToggleDown
        End With
        Controls.Add(down)

        enemy_status = New ComboBox()
        With enemy_status
            .Size = New Size(69, 24)
            .Location = New Point(target_x + 235, 175)
            .DropDownStyle = ComboBoxStyle.DropDownList
            For x = 0 To 4
                .Items.Add(status_name(x))
            Next
            .SelectedIndex = 0
            AddHandler .SelectedIndexChanged, AddressOf Calculate
        End With
        Controls.Add(enemy_status)

        armor_durability = New ComboBox()
        With armor_durability
            .Size = New Size(39, 24)
            .Location = New Point(target_x + 120, 200)
            .DropDownStyle = ComboBoxStyle.DropDownList
            AddHandler .SelectedIndexChanged, AddressOf Calculate
        End With
        Controls.Add(armor_durability)


        ' PARTY TARGET

        E_target = New PictureBox()
        With E_target
            .Hide()
            .Size = New Size(70, 70)
            .Location = New Point(target_x + 135, 14)
            .Cursor = Cursors.Hand
            AddHandler .Click, AddressOf ChangeFocus
            AddHandler .Click, AddressOf E_SwitchTarget
        End With
        Controls.Add(E_target)

        E_down = New CheckBox()
        With E_down
            .Hide()
            .Size = New Size(79, 24)
            .Location = New Point(target_x + 135 + 85, 25)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Down"
            AddHandler .CheckedChanged, AddressOf ToggleDown
        End With
        Controls.Add(E_down)

        E_status = New ComboBox()
        With E_status
            .Hide()
            .Size = New Size(81, 24)
            .Location = New Point(target_x + 135 + 84, 50)
            .DropDownStyle = ComboBoxStyle.DropDownList
            For x = 0 To 4
                .Items.Add(status_name(x))
            Next
            .SelectedIndex = 0
            AddHandler .SelectedIndexChanged, AddressOf E_Calculate
        End With
        Controls.Add(E_status)

        For x = 0 To 6
            E_target_data(x) = New Label()
            With E_target_data(x)
                .Hide()
                Select Case x
                    Case < 2
                        .Size = New Size(54, 24)
                        .Location = New Point(target_x + 135 + 115 * (x Mod 2), target_y + 75)
                    Case < 4
                        .Size = New Size(84, 24)
                        .Location = New Point(target_x + 135 + 85 * (x Mod 2), target_y + 100)
                    Case < 6
                        .Size = New Size(84, 24)
                        .Location = New Point(target_x + 135 + 85 * (x Mod 2), target_y + 125)
                    Case Else
                        .Size = New Size(84, 24)
                        .Location = New Point(target_x, target_y + 175)
                End Select
                .BackColor = default_color
                .TextAlign = ContentAlignment.MiddleCenter
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(E_target_data(x))
        Next
        E_target_data(0).Text = "HP"
        E_target_data(2).Text = "Knockdown"
        E_target_data(4).Text = "Knockout"
        E_target_data(6).Text = "Prima Queens"
        AddHandler E_target_data(1).Click, AddressOf E_ResetHP

        E_HP_box = New TextBox()
        With E_HP_box
            .Hide()
            .AutoSize = False
            .Size = New Size(59, 24)
            .Location = New Point(target_x + 135 + 55, target_y + 75)
            .MaxLength = 5
            .TextAlign = HorizontalAlignment.Center
            AddHandler .TextChanged, AddressOf E_CheckHP
            AddHandler .MouseWheel, AddressOf ScrollHP
            AddHandler .KeyPress, AddressOf FilterInput
            AddHandler .LostFocus, AddressOf FixHP
        End With
        Controls.Add(E_HP_box)

        E_secondary_target = New CheckBox()
        With E_secondary_target
            .Hide()
            .Size = New Size(169, 24)
            .Location = New Point(target_x + 135, target_y + 150)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Secondary target"
            AddHandler .CheckedChanged, AddressOf E_Calculate
        End With
        Controls.Add(E_secondary_target)

        E_armor = New CheckBox()
        With E_armor
            .Hide()
            .Size = New Size(80, 24)
            .Location = New Point(target_x + 45, target_y + 190)
            .BackColor = default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Armor"
            .Checked = True
            AddHandler .CheckedChanged, AddressOf E_Calculate
        End With
        Controls.Add(E_armor)

        E_prima_queens = New ComboBox()
        With E_prima_queens
            .Hide()
            .Size = New Size(40, 24)
            .Location = New Point(target_x + 85, target_y + 175)
            .DropDownStyle = ComboBoxStyle.DropDownList
            For x = 0 To 3
                .Items.Add(x)
            Next
            .SelectedIndex = 3
            AddHandler .SelectedIndexChanged, AddressOf E_Calculate
        End With
        Controls.Add(E_prima_queens)


        ' BUTTONS

        Dim button_resource() As String = {"_1", "_500", "_393", "dolphin", "spreadsheet", "settings"}
        For x = 0 To 5
            button(x) = New PictureBox()
            With button(x)
                If x < 3 Then
                    .Size = New Size(40, 64)
                    .Location = New Point(673 + x * 50, button_row_1_Y_pos)
                Else
                    .Size = New Size(48, 48)
                    .Location = New Point(669 + (x - 3) * 50, button_row_2_Y_pos)
                End If
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject(button_resource(x)), .Size)
                .Name = x
                .Cursor = Cursors.Hand
                AddHandler .Click, AddressOf ButtonClick
                AddHandler .MouseDown, AddressOf MoveButtonDown
                AddHandler .MouseUp, AddressOf MoveButtonUp
                AddHandler .MouseLeave, AddressOf MoveButtonUp
                AddHandler .LostFocus, AddressOf MoveButtonUp
            End With
            Controls.Add(button(x))
        Next

        combo_results = New Label()
        With combo_results
            .Size = New Size(124, 50)
            .Location = New Point(693, 150)
            .TextAlign = ContentAlignment.MiddleRight
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(combo_results)

        enemy_mode_button = New Button()
        With enemy_mode_button
            .Size = New Size(125, 30)
            .Location = New Point(520, 206)
            .UseVisualStyleBackColor = True
            .Text = "Switch to enemy"
            AddHandler .Click, AddressOf SwitchMode
        End With
        Controls.Add(enemy_mode_button)

        MP_button = New Button()
        With MP_button
            .Size = New Size(54, 30)
            .Location = New Point(668, 206)
            .UseVisualStyleBackColor = True
            .Text = "MP"
            .Name = 6
            AddHandler .Click, AddressOf ButtonClick
        End With
        Controls.Add(MP_button)

        next_combo = New Button()
        With next_combo
            .Size = New Size(96, 30)
            .Location = New Point(722, 206)
            .UseVisualStyleBackColor = True
            .Text = "Next combo"
            .Enabled = False
            AddHandler .Click, AddressOf NextCombo
        End With
        Controls.Add(next_combo)


        ' CARDS

        For x = 0 To 2
            card_panel(x) = New CustomPanel()
            With card_panel(x)
                .AutoScroll = True
                If x < 2 Then
                    .Size = New Size(1163, 100)
                    .Location = New Point(0, 250 + x * 100)
                Else
                    .Hide()
                    .Size = New Size(510, 100)
                    .Location = New Point(0, 250)
                End If
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(card_panel(x))
        Next

        For x = 0 To 299
            hand(x) = New PictureBox()
            With hand(x)
                .Hide()
                .Size = New Size(50, 80)
                .Location = New Point(10 + x * 50, 0)
                .Name = x
                .Cursor = Cursors.Hand
                AddHandler .Click, AddressOf AddCard
                AddHandler .Click, AddressOf RemoveCardFromDeck
            End With
        Next

        For x = 0 To 9
            E_hand(x) = New PictureBox()
            With E_hand(x)
                .Hide()
                .Size = New Size(50, 80)
                .Location = New Point(10 + x * 50, 0)
                .Name = x
                .Cursor = Cursors.Hand
                AddHandler .Click, AddressOf E_AddCard
                AddHandler .MouseEnter, AddressOf E_ShowAttackName
            End With
            card_panel(2).Controls.Add(E_hand(x))
        Next

        For x = 0 To 124
            combo(x) = New PictureBox()
            With combo(x)
                .Hide()
                .Size = New Size(50, 80)
                .Location = New Point(10 + x * 50, 0)
                .Name = x
                .Cursor = Cursors.Hand
                AddHandler .Click, AddressOf RemoveCard
                AddHandler .MouseEnter, AddressOf HighlightHits
                AddHandler .MouseLeave, AddressOf UnhighlightHits
            End With
        Next


        ' MP

        MP_display = New Label()
        With MP_display
            .Hide()
            .Size = New Size(67, 30)
            .Location = New Point(25, 25)
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Segoe UI", 11, FontStyle.Bold)
        End With
        card_panel(1).Controls.Add(MP_display)

        burst = New Button()
        With burst
            .Hide()
            .Size = New Size(73, 32)
            .Location = New Point(19, 24)
            .UseVisualStyleBackColor = True
            .Font = New Font("Segoe UI", 11, FontStyle.Bold)
            .Text = "Burst"
            AddHandler .Click, AddressOf ToggleBurst
        End With
        card_panel(1).Controls.Add(burst)

        dummy = New Label()
        With dummy
            .Size = New Size(0, 0)
            .Location = New Point(92, 0)
        End With
        card_panel(1).Controls.Add(dummy)


        ' OUTPUT TABLE

        output_panel = New CustomPanel()
        With output_panel
            .AutoScroll = True
            .Size = New Size(Width - 16, Height - 490)
            .Location = New Point(0, 451)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(output_panel)

        E_output_panel = New CustomPanel()
        With E_output_panel
            .Hide()
            .AutoScroll = True
            .Size = New Size(Width - 16, Height - 490)
            .Location = New Point(0, 451)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(E_output_panel)

        For y = 0 To rows - 1
            table(0, y) = New Label()
            With table(0, y)
                .Location = New Point(5, 25 * y)
                .Size = New Size(150, 24)
                .BackColor = default_color
                .TextAlign = ContentAlignment.MiddleLeft
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .Text = variable(y)
                .Tag = y
                AddHandler .MouseClick, AddressOf ChangeFocus
                AddHandler .MouseEnter, AddressOf ShowDescription
                AddHandler .Click, AddressOf ResetRow
                AddHandler .Click, AddressOf HideRow
            End With
            output_panel.Controls.Add(table(0, y))
        Next
        For x = 0 To 7
            table(0, clickable_rows(x)).Cursor = Cursors.Hand
        Next
        table(0, rows - 1).Cursor = Cursors.Hand
        If My.Settings.EffectiveHPRemaining Then
            table(0, rows - 1).Text = "Effective HP remaining"
        End If

        For x = 1 To 999
            For y = 0 To rows - 1
                If hit_modifier_row.Contains(y) Then
                    Continue For
                End If
                table(x, y) = New Label
                With table(x, y)
                    .Size = New Size(51, 24)
                    .Location = New Point(104 + 52 * x, 25 * y)
                    .BackColor = default_color
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Tag = x
                    .Name = y
                    AddHandler .MouseClick, AddressOf ChangeFocus
                    AddHandler .MouseClick, AddressOf ToggleEffect
                End With
            Next
            For y = 0 To 2
                hit_modifier(x, y) = New ComboBox
                With hit_modifier(x, y)
                    .Hide()
                    .Size = New Size(51, 24)
                    .Location = New Point(104 + x * 52, 25 * hit_modifier_row(y))
                    .Tag = x
                    .Name = y
                    .DropDownStyle = ComboBoxStyle.DropDownList
                    For i = 3 To 1 Step -1
                        .Items.Add("+" & i & "%")
                    Next
                    For i = 0 To -4 Step -1
                        .Items.Add(i & "%")
                    Next
                End With
            Next
            hit_card(x) = -1
        Next

        For y = 0 To 25
            E_table(0, y) = New Label()
            With E_table(0, y)
                .Location = New Point(5, 25 * y)
                .Size = New Size(150, 24)
                .BackColor = default_color
                .TextAlign = ContentAlignment.MiddleLeft
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .Text = E_variable(y)
                .Tag = y
                AddHandler .MouseClick, AddressOf ChangeFocus
                AddHandler .MouseEnter, AddressOf ShowDescription
                AddHandler .Click, AddressOf E_ResetRow
                AddHandler .Click, AddressOf E_HideRow
            End With
            E_output_panel.Controls.Add(E_table(0, y))
        Next
        For x = 0 To 4
            E_table(0, E_clickable_rows(x)).Cursor = Cursors.Hand
        Next

        For x = 1 To 59
            For y = 0 To 25
                If E_hit_modifier_row.Contains(y) Then
                    Continue For
                End If
                E_table(x, y) = New Label
                With E_table(x, y)
                    .Size = New Size(51, 24)
                    .Location = New Point(104 + 52 * x, 25 * y)
                    .BackColor = default_color
                    .TextAlign = ContentAlignment.MiddleCenter
                    .Tag = x
                    .Name = y
                    AddHandler .MouseClick, AddressOf ChangeFocus
                    AddHandler .MouseClick, AddressOf E_ToggleEffect
                End With
            Next
            For y = 0 To 2
                E_hit_modifier(x, y) = New ComboBox
                With E_hit_modifier(x, y)
                    .Hide()
                    .Size = New Size(51, 24)
                    .Location = New Point(104 + x * 52, 25 * E_hit_modifier_row(y))
                    .Tag = x
                    .Name = y
                    .DropDownStyle = ComboBoxStyle.DropDownList
                    For i = 3 To 1 Step -1
                        .Items.Add("+" & i & "%")
                    Next
                    For i = 0 To -4 Step -1
                        .Items.Add(i & "%")
                    Next
                End With
            Next
        Next

        hover = New ToolTip()
        With hover
            .AutomaticDelay = 250
            .AutoPopDelay = 30000
            .InitialDelay = 250
            .ReshowDelay = 50
            .Active = My.Settings.TableTooltips
        End With

        description(0) = "An offense value based on the party member's level."
        description(1) = "An offense value from an attack. Every hit within an attack has its own offense value."
        description(2) = "A crush value from an attack. Every hit within an attack has its own crush value."
        description(3) = "Some magnus grant a 2-turn offense and crush bonus."
        description(4) = "A random offense deviation. Click to reset the offense deviation on all hits."
        description(5) = "A random crush deviation. Click to reset the crush deviation on all hits."
        description(6) = "Bonus factor from Electric Helm or Blitz Helm when using lightning attacks."
        description(7) = "Additional offense from equipped weapon. Click any cell in this row to enable a mid-combo status effect for specific weapons. Click here to remove all mid-combo status effects."
        description(8) = "Additional crush from equipped weapon. Click any cell in this row to enable a mid-combo defense/offense reduction for specific weapons. Click here to remove all mid-combo defense/offense reductions."
        description(9) = "Depending on the elements of the attack and equipped weapon, only a portion of the weapon bonus may take effect."
        description(10) = "A critical hit factor that increases the strength of weapons such as Excalibur or Dragonbuster." & vbCrLf & "Cutthroat Knife has a 50% crit chance. When it is equipped, click any cell in this row to enable a critical hit. Click here to remove all Cutthroat Knife critical hits."
        description(11) = "Offense and crush bonus from quest magnus."
        description(12) = "Offense bonus from the party member's aura."
        description(13) = "Crush bonus from the party member's aura."
        description(14) = "During an EX combo, a bonus factor is applied to all the above offense values."
        description(15) = "During an EX combo, a bonus factor is applied to all the above crush values."
        description(16) = "Critical hits will apply a factor to all the above offense and crush values. Which factor is used depends on the enemy's status, the attack element, and whether or not the critical hit was caused by quest magnus (random)." & vbCrLf & "Click any cell in this row to enable a random quest magnus based critical hit. Click here to remove all random critical hits."
        description(17) = "Every enemy has six defense values - one for each element. Some bosses have a shield that will visibly and audibly break when their HP drops below a certain threshold." & vbCrLf & "When this happens, all defenses are permanently multiplied by 0.8."
        description(18) = "The resilience of the enemy's defense. When the enemy's crush status reaches this value, the total defense will be zero." & vbCrLf & "Some enemies have a shield that will visibly and audibly break when their HP drops below a certain threshold." & vbCrLf & "When this happens, the crush limit (as well as the knockdown and knockout thresholds) are permanently multiplied by 0.8."
        description(19) = "The crush status increases during a combo. The higher it gets, the lower the enemy's total defense will be on the next hit." & vbCrLf & "The ratio of the crush status to the crush limit is used as a defense factor. For instance, if crush status is 60 and crush limit is 100, only 40% of the enemy's defense will be in effect on the next hit." & vbCrLf & "When the combo ends, the crush status is reset to 0 so that the full defense will be in effect when the next combo begins."
        description(20) = "Some items or enemy moves can change the enemy's defense for two turns."
        description(21) = "A random defense deviation. Click to reset the defense deviation on all hits."
        description(22) = "The total offense after adding up all offense components and applying all their factors." & vbCrLf & "total_offense = (base_offense * attack_offense * attack_boost_factor * random_offense_factor * electric_helm_factor + weapon_offense * element_compatibility * weapon_factor + quest_magnus_bonus + aura_offense) * ex_combo_offense_factor * critical_hit_factor" & vbCrLf & "Total offense can be negative if attack_boost_factor is negative."
        description(23) = "The total crush after adding up all crush components and applying all their factors." & vbCrLf & "total_crush = (base_offense * attack_crush * attack_boost_factor * random_crush_factor * electric_helm_factor + weapon_crush * element_compatibility * weapon_factor + quest_magnus_bonus + aura_crush) * ex_combo_crush_factor * critical_hit_factor" & vbCrLf & "Total crush can be negative if attack_boost_factor is negative."
        description(24) = "The total defense after applying all defense factors." & vbCrLf & "total_defense = base_defense * (1 - crush_status / crush_limit) * defense_boost_factor * random_defense_factor" & vbCrLf & "Total defense can be negative if defense_boost_factor is negative. Total defense is 0 if crush_status >= crush_limit."
        description(25) = "Additional defense from the enemy's equipped armor."
        description(26) = "This factor is applied to the damage and crush output. It is 0.1 for machina armas before Sagi's awakening, and 0.2 when Guillo attacks a Sandfeeder."
        description(27) = "If total_offense > total_defense + armor_defense, then damage_output = (total_offense - total_defense * 0.775 - armor_defense) * multiplier" & vbCrLf & "Else damage_output = (total_offense - total_defense * 0.1 - armor_defense) * 0.25 * multiplier" & vbCrLf & "Damage output cannot be negative. If the result is less than 1, there is a 75% chance that damage output will be adjusted to 1, provided that armor_defense = 0 and multiplier = 1." & vbCrLf & "Click any cell in this row to enable a 'minimum 1' hit. Click here to disable all 'minimum 1' hits."
        description(28) = "If total_crush > total_defense * 0.5 + armor_defense, then crush_output = (total_crush - total_defense * 0.3875 - armor_defense) * multiplier" & vbCrLf & "Else crush_output = (total_crush - total_defense * 0.05 - armor_defense) * 0.25 * multiplier" & vbCrLf & "Crush output cannot be negative."
        description(29) = "The total damage output so far."
        description(30) = "The total crush output so far. Yellow cells indicate a knockdown at the end of the combo. Orange cells indicate a knockout at the end of the combo."
        description(31) = "The enemy's HP after each hit. Click to toggle effective HP for 'unbeatable' bosses. A shield break will be highlighted in pink."

        E_description(0) = "The enemy's offensive power."
        E_description(1) = "The enemy's destructive power."
        E_description(2) = description(1)
        E_description(3) = description(2)
        E_description(4) = "Some magnus or abilities alter the enemy's offense for two turns."
        E_description(5) = description(4)
        E_description(6) = description(5)
        E_description(7) = "Additional power from Machina Arma: Razer 3's armor."
        E_description(8) = "Critical hits will apply a factor to all the above offense and crush values. Which factor is used depends on the party member's status and the attack element." & vbCrLf & "Click any cell in this row to prevent a critical hit with Jiraiya's Robe (80% chance). Click here to remove all random critical hit blocks."
        E_description(9) = "The party member's defense for the element used."
        E_description(10) = "Defense bonus from quest magnus."
        E_description(11) = "The resilience of the party member's defense. When the party member's crush status reaches this value, the total defense will be zero."
        E_description(12) = "The crush status increases during a combo. The higher it gets, the lower the party member's total defense will be on the next hit." & vbCrLf & "The ratio of the crush status to the crush limit is used as a defense factor. For instance, if crush status is 60 and crush limit is 100, only 40% of the party member's defense will be in effect on the next hit." & vbCrLf & "When the combo ends, the crush status is reset to 0 so that the full defense will be in effect when the next combo begins."
        E_description(13) = "Some magnus or abilities alter the party member's defense for two turns."
        E_description(14) = description(21)
        E_description(15) = "Vanishing Cloak reduces defense by 50%."
        E_description(16) = "Defense bonus from the party member's aura."
        E_description(17) = "The total offense after applying all offense factors." & vbCrLf & "total_offense = (base_offense * attack_offense * attack_boost_factor * random_offense_factor + armor_bonus) * critical_hit_factor" & vbCrLf & "Total offense can be negative if attack_boost_factor is negative."
        E_description(18) = "The total crush after applying all crush factors." & vbCrLf & "total_crush = (base_crush * attack_crush * attack_boost_factor * random_crush_factor + armor_bonus) * critical_hit_factor" & vbCrLf & "Total crush can be negative if attack_boost_factor is negative."
        E_description(19) = "The total defense after adding up all defense components and applying all their factors." & vbCrLf & "total_defense = ((base_defense + quest_magnus_defense) * (1 - crush_status / crush_limit) * defense_boost_factor * random_defense_factor * vanishing_cloak_factor) + aura_defense" & vbCrLf & "Total defense can be negative if defense_boost_factor is negative. Total defense is 0 if crush_status >= crush_limit."
        E_description(20) = "Additional defense from the party member's equipped armor."
        E_description(21) = "If total_offense > total_defense + armor_defense, then damage_output = total_offense - total_defense * 0.775 - armor_defense" & vbCrLf & "Else damage_output = (total_offense - total_defense * 0.1 - armor_defense) * 0.25" & vbCrLf & "Damage output cannot be negative. If the result is less than 1, there is a 75% chance that damage output will be adjusted to 1." & vbCrLf & "Click any cell in this row to enable a 'minimum 1' hit. Click here to disable all 'minimum 1' hits."
        E_description(22) = "If total_crush > total_defense * 0.5 + armor_defense, then crush_output = total_crush - total_defense * 0.3875 - armor_defense" & vbCrLf & "Else crush_output = (total_crush - total_defense * 0.05 - armor_defense) * 0.25" & vbCrLf & "Crush output cannot be negative."
        E_description(23) = description(29)
        E_description(24) = description(30)
        E_description(25) = "The party member's HP after each hit."

        For x = 1 To 454
            magnus_image(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x), New Size(50, 80))
        Next
        first_hit(0) = 1
        item_target = My.Settings.PartyOrder.Substring(0, 1)

        For x = 1 To 101
            For y = 1 To 7
                If EX_combo_data(x, y) = 0 Then
                    Exit For
                End If
                EX_string(x) &= Convert.ToChar(EX_combo_data(x, y) + 34)
            Next
        Next

        AddHandler Click, AddressOf ChangeFocus
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Resize, AddressOf SaveWindowData
        AddHandler Resize, AddressOf ResizePanel
        ResizePanel()

        UpdateRows()
        With My.Settings
            If .MagnusActive Is Nothing OrElse .MagnusActive.Count <> 456 Then
                .MagnusActive = New Specialized.StringCollection
                .MagnusActive.Clear()
                deck_magnus(0) = "0"
                For x = 1 To 44
                    deck_magnus(x) = "1"    'all attacks
                Next
                For x = 45 To 454
                    deck_magnus(x) = "0"
                Next
                deck_magnus(58) = "1"       'Laevateinn the Flameking
                deck_magnus(72) = "1"       'Ascalon
                deck_magnus(131) = "1"      'Drakeshead Stave
                deck_magnus(139) = "1"      'Vajra the Indestructible
                deck_magnus(167) = "1"      'Ice Roue
                deck_magnus(169) = "1"      'Deluge the Seabane
                deck_magnus(195) = "1"      'Amor Alma
                deck_magnus(238) = "1"      'Firedrake Regalia
                deck_magnus(351) = "1"      'Round Shield
                .MagnusActive.AddRange(deck_magnus)
            End If
            Dim temp() As Char = String.Join("", .MagnusActive.OfType(Of String)).ToCharArray
            For x = 0 To 454
                deck_magnus(x) = temp(x)
            Next

            level_selector(0).Text = .SagiLevel
            level_selector(1).Text = .MillyLevel
            level_selector(2).Text = .GuilloLevel
            Dim type, level As Integer
            For x = 0 To 2
                type = "&H" & .Auras.ElementAt(x * 2)
                type = Clamp(type, 0, 11)
                aura_type(x).SelectedIndex = type
                level = "&H" & .Auras.ElementAt(x * 2 + 1)
                level = Clamp(level, 0, 2)
                aura_level(x).SelectedIndex = level
            Next
            Dim qm As Integer
            For x = 0 To 23
                qm = "&H" & .QuestMagnus.Substring(x * 2, 2)
                qm = Clamp(qm, 0, 180)
                QM_inventory(x) = qm
            Next
            CheckQuestMagnus()
            Show()
            ChangeTarget(.Target, -1, False)
            final_phase.Checked = .FinalPhase
            SwitchCharacter(char_image(.Character), New EventArgs)
        End With
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.WindowSize
        Dim pt As Point = My.Settings.WindowLocation
        If pt.X = -1 And pt.Y = -1 Then
            CenterToScreen()
            Return
        End If
        For Each s As Screen In Screen.AllScreens
            If s.Bounds.Contains(pt) Then
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub SaveWindowData()
        If WindowState = FormWindowState.Normal Then
            My.Settings.WindowSize = Size
            My.Settings.WindowLocation = Location
        End If
    End Sub

    Private Sub ChangeFocus(sender As Object, e As EventArgs)
        If sender Is Me Then
            target_image.Focus()
            Return
        End If
        sender.Focus()
    End Sub

    Private Sub SwitchMode()
        If cards > 0 Then
            RemoveCard(combo(0), New MouseEventArgs(0, 0, 0, 0, 0))
        End If
        enemy_mode = Not enemy_mode
        Dim target_x As Integer = 340
        Dim target_y As Integer = 15
        If Not enemy_mode Then
            target_data(0).Hide()
            E_target.Hide()
            E_down.Hide()
            E_status.Hide()
            For x = 0 To 6
                E_target_data(x).Hide()
            Next
            E_HP_box.Hide()
            E_secondary_target.Hide()
            E_armor.Hide()
            E_prima_queens.Hide()
            card_panel(2).Hide()
            card_panel(0).Location = New Point(0, 250)
            For x = 1 To 8
                target_data(x).Show()
            Next
            E_output_panel.Hide()
            down.Show()
            enemy_status.Show()
            enemy_HP.Show()
            secondary_target.Show()
            With target_data(0)
                .Location = New Point(target_x + 135, target_y - 1)
                .Size = New Size(169, 25)
                .Show()
            End With
            ChangeTarget(combo_target, true_HP, False)
            output_panel.Show()
            If MP.Visible Then
                DisplayMP(current_MP)
            End If
            enemy_mode_button.Text = "Switch to enemy"
            Target.Text = "Target"
        Else
            For x = 1 To 12
                target_data(x).Hide()
            Next
            down.Hide()
            enemy_status.Hide()
            enemy_HP.Hide()
            final_phase.Hide()
            shield.Hide()
            secondary_target.Hide()
            armor_durability.Hide()
            target_data(0).Hide()
            output_panel.Hide()
            If MP.Visible Then
                MP_display.Hide()
                burst.Hide()
            End If
            E_target.Show()
            E_down.Show()
            E_status.Show()
            E_HP_box.Show()
            For x = 0 To 5
                E_target_data(x).Show()
            Next
            E_secondary_target.Show()
            If combo_target = 117 Then
                E_armor.Show()
            End If
            If combo_target = 70 Then
                E_target_data(6).Show()
                E_prima_queens.Show()
            End If
            E_target.Image = char_icon(character - 1)
            With target_data(0)
                .Location = New Point(target_x, target_y + 150) ' + 179
                Dim text_width As Integer = TextRenderer.MeasureText(.Text, .Font).Width
                If text_width <= 125 Then
                    .Size = New Size(125, 24)
                Else
                    .Size = New Size(125, 39)
                End If
                .Show()
            End With
            GetPartyData()
            card_panel(0).Location = New Point(360, 250)
            card_panel(2).Show()
            E_output_panel.Show()
            E_UpdateRows()
            Target.Text = "Enemy"
            enemy_mode_button.Text = "Switch to party"
        End If
        If QuestMagnus.Visible Then
            QuestMagnus.SwitchMode()
        End If
        If Boost.Visible Then
            Boost.SwitchMode()
        End If
        If Settings.Visible Then
            Settings.SwitchMode()
        End If
        ShowDeck()
        E_ShowDeck()
        ResizePanel()
        ScrollToStart()
        If ActiveForm Is Me Then
            target_image.Focus()
        End If
    End Sub

    Private Sub Dolphin()
        Dim emulator As Process() = Process.GetProcessesByName("Dolphin")
        If emulator.Length = 0 Then
            MsgBox("Dolphin isn't open.")
            Return
        End If
        If emulator.Length > 1 Then
            MsgBox("Please use only one instance of Dolphin.")
            Return
        End If
        Dim title As String = emulator(0).MainWindowTitle
        Dim version As Integer
        Dim dolphin_version() As String = {"Dolphin 4.0", "Dolphin 4.0.1", "Dolphin 4.0.2", "Dolphin 5.0"}
        For x = 0 To 3
            If title = dolphin_version(x) OrElse title.StartsWith(dolphin_version(x) & " |") Then
                version = x + 1
                Exit For
            End If
        Next
        Select Case version
            Case 1 To 3
                dolphin_offset = &H7FFF0000        '4.0, 4.0.1, 4.0.2
            Case 4
                dolphin_offset = -&H10000          '5.0
            Case 0
                Dim secondary_window() As String = {"TAS Input", "Memory Card Manager", "Cheat Manager", "Cheats Manager", "Dolphin NetPlay Setup", "FIFO Player"}
                For x = 0 To secondary_window.Length - 1
                    If title.StartsWith(secondary_window(x)) Then
                        MsgBox("Please close " & secondary_window(x) & " and try again.")
                        Return
                    End If
                Next
                MsgBox("This version of Dolphin is not supported. Please use Dolphin 5.0 or 4.0.x.")
                Return
        End Select
        hProcess = OpenProcess(PROCESS_ALL_ACCESS, 0, emulator(0).Id)
        If hProcess = IntPtr.Zero Then
            MsgBox("Failed to open Dolphin.")
            Return
        End If

        Dim game, level_address, aura_address, quest_magnus_address, deck_class_address, pointer_address, battle_id_address, combo_address, mp_address, offset As UInteger
        game = Read32U(&H80000000L)
        If game = &H474B344A Then               'GK4J (Japanese version)
            My.Settings.EnglishVersion = False
            level_address = &H802DD15EL
            aura_address = &H802DD1E8L
            quest_magnus_address = &H802E24F2L
            deck_class_address = &H802DD3DFL
            pointer_address = &H8086C064L
            battle_id_address = &H802DDB26L
            combo_address = &H802E6D86L
            mp_address = &H8030B550L
            offset = &H1118C
        ElseIf game = &H474B3445 Then           'GK4E (English version)
            My.Settings.EnglishVersion = True
            level_address = &H802D5466L
            aura_address = &H802D54F0L
            quest_magnus_address = &H802DAA9AL
            deck_class_address = &H802D56E7L
            pointer_address = &H80857B84L
            battle_id_address = &H802D5E2EL
            combo_address = &H802E3F4EL
            mp_address = &H8030BC38L
            offset = &H183A4
        Else
            CloseHandle(hProcess)
            If game = 0 Then
                MsgBox("No game running.")
            Else
                MsgBox("Wrong game.")
            End If
            Return
        End If

        locked = True

        'game version
        If Settings.Visible Then
            With Settings.setting(4)
                RemoveHandler .CheckedChanged, AddressOf Settings.ChangeSetting
                .Checked = My.Settings.EnglishVersion
                AddHandler .CheckedChanged, AddressOf Settings.ChangeSetting
            End With
        End If

        'levels
        For x = 0 To 2
            Me.level(x) = Read16(level_address + x * 244)
            Me.level(x) = Clamp(Me.level(x), 1, 100)
            level_selector(x).SelectedIndex = Me.level(x) - 1
        Next

        'auras
        Dim type, level As Integer
        For x = 0 To 2
            type = Read32(aura_address + x * 244)
            type = Clamp(type, 0, 11)
            aura_type(x).SelectedIndex = type
            If type > 0 Then
                level = Read32(aura_address + 4 + x * 244)
                level = Clamp(level, 1, 3)
                aura_level(x).SelectedIndex = level - 1
            End If
        Next

        'quest magnus
        Dim id As Integer
        For x = 0 To 23
            id = Read32(quest_magnus_address + x * 152)
            Select Case id
                Case < 500
                    id = 0      'not a quest magnus
                Case < 593
                    id -= 500
                Case < 610
                    id = 0      'placeholder magnus
                Case < 670
                    id -= 517
                Case < 672
                    id = 0      'unused magnus
                Case < 700
                    id -= 519
                Case Else
                    id = 0
            End Select
            QM_inventory(x) = id
        Next
        If QuestMagnus.Visible Then
            QuestMagnus.move_slot = -1
            For x = 0 To 23
                QuestMagnus.inventory(x).Tag = QM_inventory(x)
                QuestMagnus.inventory(x).Image = QuestMagnus.magnus(QM_inventory(x))
            Next
        End If
        CheckQuestMagnus()

        'deck class
        If MP.Visible Then
            Dim deck_class As Integer = Read8(deck_class_address)
            deck_class = Clamp(deck_class, 1, 30)
            MP.class_selector.SelectedIndex = deck_class - 1
        End If

        Dim pointer As UInteger = Read32U(pointer_address)
        If pointer <> 0 AndAlso Read16(pointer + 148) = 3 Then
            ReadBattleData(pointer + offset, battle_id_address, combo_address, mp_address)
        End If

        CloseHandle(hProcess)
        locked = False
        Calculate()
    End Sub

    Private Sub ReadBattleData(battle_address As UInteger, battle_id_address As UInteger, combo_address As UInteger, mp_address As UInteger)
        secondary_target.Checked = False

        Dim battle_id, enemy_HP(5), party(3), party_size, prepared_turns, active_turns, prepared_turn_type(3), active_turn_type(3), enemy_party_size, enemy_party(5) As Integer
        Dim prepared_turn(3), active_turn(3), targeted(3), current_target As Int64
        Dim first_card(3), next_card(3) As UInteger
        Dim enemy_defense_boost(5, 6, 2), enemy_offense_boost(5, 6, 2) As Double

        battle_id = Read16(battle_id_address)

        current_target = Read32U(battle_address + &HC90)
        If current_target <> 0 Then
            current_target = (current_target - battle_address - &H1E4) / &H1494 + 9
        End If

        Dim value_1, value_2 As Double
        party_size = Read16(battle_address - &HE346)
        For x = 0 To party_size - 1
            party(x) = Read16(battle_address - &HD02E + x * &H1578)
            For y = 0 To 5
                value_1 = ReadFloat(battle_address - &HD15C + x * &H1578 + y * 4)
                value_2 = ReadFloat(battle_address - &HD10C + x * &H1578 + y * 4)
                If Not Boost.Visible Then
                    offense_boost(party(x) - 1, y, 0) = LimitBoost(value_1)
                    offense_boost(party(x) - 1, y, 1) = LimitBoost(value_2)
                ElseIf Not enemy_mode Then
                    Boost.boost(party(x) - 1, y, 0).Text = LimitBoost(value_1)
                    Boost.boost(party(x) - 1, y, 1).Text = LimitBoost(value_2)
                End If
                value_1 = ReadFloat(battle_address - &HD144 + x * &H1578 + y * 4)
                value_2 = ReadFloat(battle_address - &HD0F4 + x * &H1578 + y * 4)
                If Not Boost.Visible Then
                    defense_boost(party(x) - 1, y, 0) = LimitBoost(value_1)
                    defense_boost(party(x) - 1, y, 1) = LimitBoost(value_2)
                ElseIf enemy_mode Then
                    Boost.boost(party(x) - 1, y, 0).Text = LimitBoost(value_1)
                    Boost.boost(party(x) - 1, y, 1).Text = LimitBoost(value_2)
                End If
            Next
        Next

        enemy_party_size = Read16(battle_address - &HA2DA)
        For x = 0 To enemy_party_size - 1
            enemy_party(x) = Read16(battle_address - &H8F26 + x * &H1494)
            enemy_HP(x) = Read16(battle_address - &HA21A + x * &H1494)
            For y = 0 To 5
                enemy_defense_boost(x, y, 0) = ReadFloat(battle_address - &H90D4 + x * &H1494 + y * 4)
                enemy_defense_boost(x, y, 1) = ReadFloat(battle_address - &H9084 + x * &H1494 + y * 4)
                enemy_offense_boost(x, y, 0) = ReadFloat(battle_address - &H90EC + x * &H1494 + y * 4)
                enemy_offense_boost(x, y, 1) = ReadFloat(battle_address - &H909C + x * &H1494 + y * 4)
            Next
        Next

        'get rid of expired auras
        For x = 0 To party_size - 1
            If aura(party(x) - 1, 0) > 0 AndAlso Read32U(battle_address - &HD08C + x * &H1578) = 0 Then
                aura_type(party(x) - 1).SelectedIndex = 0
                aura_level(party(x) - 1).SelectedIndex = 0
            End If
        Next

        Dim combo_damage As Integer
        Dim final_target As Integer = current_target - 1
        Dim enemy As Integer

        If enemy_mode And My.Settings.ReadCombo Then
            Dim current_turn As Int64 = Read32U(battle_address + &H8C4)
            If current_turn = 0 Then
                Return
            End If
            current_turn = (current_turn - battle_address + &HA240) / &H1494
            If current_turn < 0 Or current_turn > enemy_party_size - 1 Then
                Return
            End If

            Dim attack_address As UInteger = battle_address - &HA1EC + current_turn * &H1494
            Dim target As Int64 = Read32U(attack_address - 32)
            If target = 0 Then
                Return
            End If
            target = (target - battle_address + &HE32C) / &H1578
            If target < 0 Or target > party_size - 1 Then
                Return
            End If

            Dim member As Integer = party(target) - 1
            member = Clamp(member, 0, 2)
            SwitchCharacter(char_image(member), New EventArgs)

            enemy = Clamp(enemy_party(current_turn), 1, 145)
            ChangeTarget(enemy, enemy_HP(current_turn), False)

            combo_damage = Read32(combo_address - 14)
            Dim hp As Integer = Read16(battle_address - &HE28A + target * &H1578) + combo_damage
            If hp < 0 Or hp > member_max_hp_armor Then
                E_HP_box.Text = member_max_hp_armor
            Else
                E_HP_box.Text = hp
            End If

            If final_phase_battles.Contains(battle_id) Then
                final_phase.Checked = True
            ElseIf final_phase.Visible Then
                final_phase.Checked = False
            End If

            If Not Boost.Visible Then
                For y = 0 To 5
                    Me.enemy_defense_boost(y, 0) = LimitBoost(enemy_defense_boost(current_turn, y, 0))
                    Me.enemy_defense_boost(y, 1) = LimitBoost(enemy_defense_boost(current_turn, y, 1))
                    Me.enemy_offense_boost(y, 0) = LimitBoost(enemy_offense_boost(current_turn, y, 0))
                    Me.enemy_offense_boost(y, 1) = LimitBoost(enemy_offense_boost(current_turn, y, 1))
                Next
            Else
                For y = 0 To 5
                    Boost.boost(3, y, 0).Text = LimitBoost(enemy_defense_boost(current_turn, y, 0))
                    Boost.boost(3, y, 1).Text = LimitBoost(enemy_defense_boost(current_turn, y, 1))
                    Boost.boost(4, y, 0).Text = LimitBoost(enemy_offense_boost(current_turn, y, 0))
                    Boost.boost(4, y, 1).Text = LimitBoost(enemy_offense_boost(current_turn, y, 1))
                Next
            End If

            If cards > 0 Then
                locked = False
                RemoveCard(Me.combo(0), New MouseEventArgs(0, 0, 0, 0, 0))
                locked = True
            End If

            Dim length As Integer = Read16(attack_address - 10)
            length = Clamp(length, 0, 10)
            For x = 0 To length - 1
                Dim attack As Integer = Read16(attack_address + x * 268 + 2)
                Dim card As Integer = -1

                If attack = 0 Then
                    Continue For
                End If
                For y = 0 To 5
                    If attack = enemy_attack_data(combo_target, y, 0) Then
                        card = y
                        Exit For
                    End If
                Next
                If card = -1 Then
                    Exit For
                End If

                Select Case combo_target
                    Case 64, 65, 66
                        Dim element As Integer = Read16(attack_address + x * 268 + 14)
                        If element = 1 Then
                            card += 3
                        End If
                    Case 68
                        Dim element As Integer = Read16(attack_address + x * 268 + 14)
                        If element = 1 Then
                            card = 5
                        End If
                    Case 70
                        If attack = 1000 Then
                            Dim offense As Double = ReadFloat(attack_address + x * 268 + 20)
                            Dim prima_queens As Integer = (offense - 20) / 15
                            prima_queens = Clamp(prima_queens, 0, 3)
                            E_prima_queens.SelectedIndex = prima_queens
                        End If
                End Select

                If E_hand(card).Visible Then
                    E_AddCard(E_hand(card), New MouseEventArgs(0, 0, 0, 0, 0))
                End If
            Next
        End If
        If enemy_mode Then
            Return
        End If

        If My.Settings.ReadCombo Then
            If cards > 0 Then
                locked = False
                RemoveCard(combo(0), New MouseEventArgs(0, -1, 0, 0, 0))
                locked = True
            End If

            For x = 0 To party_size - 1
                targeted(x) = Read32U(battle_address - &HE27C + x * &H1578)
                If targeted(x) <> 0 Then
                    targeted(x) = (targeted(x) - battle_address - &H1E4) / &H1494 + 9
                End If
                first_card(x) = Read32U(battle_address - &HCE08 + x * &H1578)
                next_card(x) = Read32U(battle_address - &HCF48 + x * &H1578)
                prepared_turn_type(x) = Read16(battle_address - &HCDFE + x * &H1578)
                active_turn_type(x) = Read16(battle_address - &HE28E + x * &H1578)
            Next

            active_turns = Read16(battle_address + &H896)
            prepared_turns = Read16(battle_address + &H916)

            Dim current_turn As Int64 = Read32U(battle_address + &H8C4)
            If current_turn <> 0 Then
                current_turn = (current_turn - battle_address - &H978) / &H1578 + 12
            End If

            Dim enemy_turns As Integer
            For x = 0 To prepared_turns - 1
                Dim y As Integer = x - enemy_turns
                prepared_turn(y) = Read32U(battle_address + &H918 + x * 4)
                If prepared_turn(y) <> 0 Then
                    prepared_turn(y) = (prepared_turn(y) - battle_address - &H978) / &H1578 + 12
                End If
                If prepared_turn(y) = 0 Or prepared_turn(y) > 3 Then
                    enemy_turns += 1
                End If
            Next
            prepared_turns -= enemy_turns

            enemy_turns = 0
            For x = 0 To active_turns - 1
                Dim y As Integer = x - enemy_turns
                active_turn(y) = Read32U(battle_address + &H898 + x * 4)
                If active_turn(y) <> 0 Then
                    active_turn(y) = (active_turn(y) - battle_address - &H978) / &H1578 + 12
                End If
                If active_turn(y) = 0 Or active_turn(y) > 3 Then
                    enemy_turns += 1
                End If
            Next
            active_turns -= enemy_turns

            Dim combo_length As Integer = Read16(combo_address - 20)
            If combo_length > 0 Then
                combo_damage = Read32(combo_address - 14)
            End If
            Dim combo_card(125) As Integer
            For x = 0 To combo_length - 1
                combo_card(x) = Read16(combo_address + x * 4)
                member(x) = 0
            Next
            For x = 0 To combo_length - 1
                For y = x To combo_length - 1
                    If magnus_user(combo_card(y)) <> 0 Then
                        member(x) = magnus_user(combo_card(y))
                        Exit For
                    End If
                Next
            Next

            'cards and order
            Dim card_id(60), slot As Integer
            Dim next_(60) As UInteger
            For x = 0 To 59
                card_id(x) = Read16(battle_address + 6 + x * 36)
                If card_id(x) = 0 Then
                    Exit For
                End If
                next_(x) = Read32U(battle_address + x * 36)
                If next_(x) <> 0 Then
                    next_(x) = (next_(x) - battle_address) / 36
                Else
                    next_(x) = UInteger.MaxValue
                End If
            Next

            Dim char_has_turn(3) As Boolean
            Dim end_of_combo As Boolean
            Dim next_turn As Integer
            'if the game has already stored part of the combo, start with that
            If combo_length > 0 Then
                For x = 0 To combo_length - 1
                    If member(x) = 0 Then
                        member(x) = party(current_turn - 1)
                        end_of_combo = True
                    End If
                    ShowCard(combo_card(x), member(x))
                    Dim chr As Integer = Array.IndexOf(party, member(x))
                    char_has_turn(chr) = True
                Next
                next_turn = 1
            End If

            If turns = -1 Then
                turns = 0
                end_of_combo = True
            End If

            'check turns in the action bar
            If Not end_of_combo Then
                For x = next_turn To active_turns - 1
                    Dim chr As Integer = active_turn(x) - 1
                    If turns > 0 AndAlso active_turn_type(chr) <> 4 Then    'type 4: relay
                        end_of_combo = True
                        Exit For
                    End If
                    If next_card(chr) = 0 Then
                        end_of_combo = True
                        Exit For
                    End If
                    slot = (next_card(chr) - battle_address) / 36
                    For y = cards To cards + 8
                        ShowCard(card_id(slot), party(chr))
                        If next_(slot) = UInteger.MaxValue Then
                            Exit For
                        End If
                        slot = next_(slot)
                    Next
                    char_has_turn(chr) = True
                Next
            End If

            If turns = -1 Then
                turns = 0
                end_of_combo = True
            End If

            'read turns that are fully prepared, but not in the action bar yet
            If Not end_of_combo Then
                For x = 0 To prepared_turns - 1
                    Dim chr As Integer = prepared_turn(x) - 1
                    If turns > 0 AndAlso prepared_turn_type(chr) <> 4 Then
                        end_of_combo = True
                        Exit For
                    End If
                    If next_card(chr) = 0 Then
                        end_of_combo = True
                        Exit For
                    End If
                    slot = (next_card(chr) - battle_address) / 36
                    For y = cards To cards + 8
                        ShowCard(card_id(slot), party(chr))
                        If next_(slot) = UInteger.MaxValue Then
                            Exit For
                        End If
                        slot = next_(slot)
                    Next
                    char_has_turn(chr) = True
                Next
            End If

            If turns = -1 Then
                turns = 0
                end_of_combo = True
            End If

            Dim last_turn As Integer = -2
            If Not end_of_combo Then
                'read the turn that is being prepared
                For x = 0 To party_size - 1
                    If first_card(x) = 0 Then
                        Continue For
                    End If
                    If active_turns > 0 And prepared_turn_type(x) <> 4 Then
                        Continue For
                    End If
                    slot = (first_card(x) - battle_address) / 36
                    For y = cards To cards + 8
                        ShowCard(card_id(slot), party(x))
                        If next_(slot) = UInteger.MaxValue Then
                            Exit For
                        End If
                        slot = next_(slot)
                    Next
                    char_has_turn(x) = True
                    last_turn = -1
                    Exit For
                Next

                'if no turn is being prepared, check if there is another prepared turn at the end of the queue
                If last_turn = -2 Then
                    For x = 0 To party_size - 1
                        If char_has_turn(x) Or next_card(x) = 0 Then
                            Continue For
                        End If
                        If prepared_turns = 0 OrElse x = prepared_turn(prepared_turns - 1) - 1 Then
                            last_turn = x
                            Exit For
                        End If
                        If active_turns = 0 OrElse x = active_turn(active_turns - 1) - 1 Then
                            last_turn = x
                            Exit For
                        End If
                    Next
                End If

                'if there is one more prepared turn, read it now
                If last_turn >= 0 AndAlso (prepared_turn_type(last_turn) = 4 Or (prepared_turn_type(last_turn) = 3 And cards = 0)) Then
                    slot = (next_card(last_turn) - battle_address) / 36
                    For x = cards To cards + 8
                        ShowCard(card_id(slot), party(last_turn))
                        If next_(slot) = UInteger.MaxValue Then
                            Exit For
                        End If
                        slot = next_(slot)
                    Next
                End If
            End If

            If cards > 0 Then
                character = member(cards - 1)
                ShowDeck()
                ScrollToStart()
                If card_panel(1).HorizontalScroll.Visible Then
                    card_panel(1).PerformLayout()
                End If
                Dim final_member As Integer = Array.IndexOf(party, character)
                If targeted(final_member) > 0 Then
                    final_target = targeted(final_member) - 1
                End If
            End If
        End If

        If final_target = 6 Then    'Machinanguis B
            final_target = 2
        End If
        final_target = Clamp(final_target, 0, enemy_party_size - 1)
        enemy = Clamp(enemy_party(final_target), 1, 145)

        ChangeTarget(enemy, enemy_HP(final_target) + combo_damage, False)

        If final_phase_battles.Contains(battle_id) Then
            final_phase.Checked = True
        ElseIf final_phase.Visible Then
            final_phase.Checked = False
        End If

        If Not Boost.Visible Then
            For y = 0 To 5
                Me.enemy_defense_boost(y, 0) = LimitBoost(enemy_defense_boost(final_target, y, 0))
                Me.enemy_defense_boost(y, 1) = LimitBoost(enemy_defense_boost(final_target, y, 1))
                Me.enemy_offense_boost(y, 0) = LimitBoost(enemy_offense_boost(final_target, y, 0))
                Me.enemy_offense_boost(y, 1) = LimitBoost(enemy_offense_boost(final_target, y, 1))
            Next
        Else
            For y = 0 To 5
                Boost.boost(3, y, 0).Text = LimitBoost(enemy_defense_boost(final_target, y, 0))
                Boost.boost(3, y, 1).Text = LimitBoost(enemy_defense_boost(final_target, y, 1))
                Boost.boost(4, y, 0).Text = LimitBoost(enemy_offense_boost(final_target, y, 0))
                Boost.boost(4, y, 1).Text = LimitBoost(enemy_offense_boost(final_target, y, 1))
            Next
        End If

        If MP.Visible Then
            current_MP = ReadFloat(mp_address)
            current_MP = Clamp(current_MP, 0, MP.max_MP)
            MP.MP.Text = current_MP
            DisplayMP(current_MP)
        End If
    End Sub

    Private Sub ShowCard(id As Integer, member As Integer)
        If cards = 125 Then
            Return
        End If
        If IsItem(id) Or id = 0 Then
            turns = -1
            Return
        End If

        combo(cards).Image = magnus_image(id)
        combo(cards).Tag = id
        If Not card_panel(1).Contains(combo(cards)) Then
            combo(cards).Left += card_panel(1).AutoScrollPosition.X
            card_panel(1).Controls.Add(combo(cards))
        End If
        combo(cards).Show()
        Me.member(cards) = member

        cards += 1
        UpdateTurns()
    End Sub

    Private Function Read8(address As UInteger) As Integer
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin_offset, buffer, 1, 0)
        Return buffer
    End Function

    Public Function Read16(address As UInteger) As Integer
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin_offset, buffer, 2, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToInt16(bytes, 2)
    End Function

    Public Function Read32(address As UInteger) As Integer
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin_offset, buffer, 4, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToInt32(bytes, 0)
    End Function

    Public Function Read32U(address As UInteger) As UInteger
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin_offset, buffer, 4, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return BitConverter.ToUInt32(bytes, 0)
    End Function

    Public Function ReadFloat(address As UInteger) As Double
        Dim buffer As Integer
        ReadProcessMemory(hProcess, address + dolphin_offset, buffer, 4, 0)
        Dim bytes() As Byte = BitConverter.GetBytes(buffer)
        Array.Reverse(bytes)
        Return Round(BitConverter.ToSingle(bytes, 0))
    End Function

    Private Function GetEquipment(eq As Integer) As Integer()
        Dim armor_equipped, weapon_element, weapon_offense, weapon_crush As Integer
        Dim weapon_factor As Double
        If IsArmor(eq) Or IsAccessory(eq) Then
            armor_equipped = True
            weapon_element = 0
        Else
            weapon_element = magnus_element(eq)
        End If
        weapon_offense = Me.weapon_offense(eq)
        weapon_crush = Me.weapon_crush(eq)
        If eq = 79 Then                                               'Excalibur, 100% chance of critical hit
            weapon_factor = 2
        ElseIf eq = 109 And enemy_type(combo_target) = 1 Then         'True Glimmer, 100% chance of critical hit on human enemy
            weapon_factor = 1.5
        ElseIf eq = 118 And enemy_type(combo_target) = 2 Then         'Apostolos Duo, 100% chance of critical hit on undead enemy
            weapon_factor = 1.5
        ElseIf eq = 120 And enemy_type(combo_target) = 3 Then         'Dragonbuster, 100% chance of critical hit on dragon
            weapon_factor = 1.5
        ElseIf eq = 157 And enemy_type(combo_target) = 4 Then         'Auriferous Stave, 100% chance of critical hit on aerial enemy
            weapon_factor = 1.5
        Else
            weapon_factor = 1
        End If
        Return {armor_equipped, weapon_element, weapon_offense, weapon_crush, weapon_factor * 2}
    End Function

    Public Sub Calculate()
        If locked Then
            Return
        End If
        If enemy_mode Then
            E_Calculate()
            Return
        End If

        Dim eq, attack, attack_element, offense_deviation, crush_deviation, weapon_element, durability, weapon_offense, weapon_crush, effect_element, boost_element, qm_bonus, aura_offense, aura_crush, base_defense, crush_limit, defense_deviation, armor_defense, armor_durability, damage_output, total_damage, true_HP, effective_HP, HP_remaining, knockdown, knockout As Integer
        Dim offense, attack_offense, attack_crush, attack_boost_factor, armor_factor, element_compatibility, weapon_factor, ex_offense_factor, ex_crush_factor, crit_factor, crush_status, defense_boost_factor, total_offense, total_crush, total_defense, multiplier, crush_output, defense_boost(6, 2), enemy_offense_boost(6, 2) As Double
        Dim full_turn_weapon, armor_equipped, reset_status, secondary_target_defeated As Boolean

        For x = 0 To 2
            turns_per_member(x) = 0
        Next
        Dim hits_prev As Integer = hits
        For x = 1 To hits_prev
            For y = 0 To 2
                RemoveHandler hit_modifier(x, y).SelectedIndexChanged, AddressOf ChangeDeviation
            Next
        Next
        hits = 0
        true_HP = Me.true_HP
        effective_HP = Math.Max(0, true_HP + effective_max_HP - true_max_HP)
        crush_status = 0
        If secondary_target.Checked And true_HP = 0 Then
            secondary_target_defeated = True
        End If
        Dim enemy_status As Integer = Me.enemy_status.SelectedIndex
        For x = 0 To 5
            For y = 0 To 1
                defense_boost(x, y) = Me.enemy_defense_boost(x, y)
                enemy_offense_boost(x, y) = Me.enemy_offense_boost(x, y)
            Next
        Next
        Dim first_heavenlapse_hit As Integer = My.Settings.Heavenlapse.IndexOf("1") + 1
        Dim shield_limit As Integer = Math.Floor(true_max_HP * Me.shield_limit(combo_target))
        shield_break_hit = 0
        Dim member As Integer = Me.member(0) - 1
        Dim prev_member As Integer

        'check pre-equipped magnus for the first turn
        If member + 1 > 0 AndAlso equipment(member).Tag > 0 AndAlso IsAttack(combo(0).Tag) Then
            eq = equipment(member).Tag
            equip(0) = eq
            Dim temp() As Integer = GetEquipment(eq)
            armor_equipped = temp(0)
            weapon_element = temp(1)
            weapon_offense = temp(2)
            weapon_crush = temp(3)
            weapon_factor = temp(4) / 2
            If eq_durability(member).Text <> "" Then
                durability = eq_durability(member).Text
            Else
                durability = 0
            End If
            If durability = 0 And Not armor_equipped Then
                full_turn_weapon = True
            Else
                full_turn_weapon = False
            End If
        Else
            equip(0) = 0
        End If

        'set armor durability for Armored Cancerite, Armored Balloona, Armored Mite, or Phoelix
        If Me.armor_durability.Visible Then
            armor_durability = Me.armor_durability.SelectedItem
        End If

        If cards > 0 Then
            turns_per_member(member) += 1
        End If

        'step through combo card by card
        For x = 0 To cards - 1
            Dim id As Integer = combo(x).Tag
            member = Me.member(x) - 1

            'secondary targets die instantly from Arabesque if their HP is 0, and any follow-up attacks won't hit
            If secondary_target.Checked And true_HP = 0 And x > 0 AndAlso combo(x - 1).Tag = 24 Then
                secondary_target_defeated = True
            End If

            If Not IsAttack(id) Then
                'save previous equipment for next combo
                If x > 0 Then
                    prev_member = Me.member(x - 1) - 1
                    If durability > 0 Or armor_equipped Then
                        post_combo_equip(prev_member, 0) = equip(x - 1)
                        post_combo_equip(prev_member, 1) = durability
                    Else
                        post_combo_equip(prev_member, 0) = 0
                        post_combo_equip(prev_member, 1) = 0
                    End If
                End If

                'get weapon stats upon equip
                eq = id
                equip(x) = eq
                Dim temp() As Integer = GetEquipment(eq)
                armor_equipped = temp(0)
                weapon_element = temp(1)
                weapon_offense = temp(2)
                weapon_crush = temp(3)
                weapon_factor = temp(4) / 2
                durability = Me.durability(id)
                If durability = 0 And Not armor_equipped Then
                    full_turn_weapon = True
                Else
                    full_turn_weapon = False
                End If

                If x > 0 Then
                    turns_per_member(member) += 1
                End If
                first_hit(x + 1) = hits + 1                     'save ID of first hit for the next card

                Continue For
            End If

            'change weapon stats upon changing members
            If x > 0 AndAlso member <> Me.member(x - 1) - 1 Then
                turns_per_member(member) += 1

                'secondary targets die instantly at the end of a turn if their HP is 0, and any follow-up attacks won't hit
                If secondary_target.Checked And true_HP = 0 Then
                    secondary_target_defeated = True
                End If

                'save equips and durability for the next combo
                prev_member = Me.member(x - 1) - 1
                If durability > 0 Or armor_equipped Then
                    post_combo_equip(prev_member, 0) = equip(x - 1)
                    post_combo_equip(prev_member, 1) = durability
                Else
                    post_combo_equip(prev_member, 0) = 0
                    post_combo_equip(prev_member, 1) = 0
                    equip(x) = 0
                End If

                equip(x) = 0
                durability = 0
                weapon_offense = 0
                weapon_crush = 0
                weapon_element = 0
                weapon_factor = 1
                full_turn_weapon = False
                armor_equipped = False

                'check pre-equipped magnus for the current turn
                If turns_per_member(member) = 1 And equipment(member).Tag > 0 Then
                    eq = equipment(member).Tag
                    equip(x) = eq
                    Dim temp() As Integer = GetEquipment(eq)
                    armor_equipped = temp(0)
                    weapon_element = temp(1)
                    weapon_offense = temp(2)
                    weapon_crush = temp(3)
                    weapon_factor = temp(4) / 2
                    If eq_durability(member).Text <> "" Then
                        durability = eq_durability(member).Text
                    Else
                        durability = 0
                    End If
                    If durability = 0 And Not armor_equipped Then
                        full_turn_weapon = True
                    Else
                        full_turn_weapon = False
                    End If
                End If

                'check for equipment magnus from earlier in the combo
                If turns_per_member(member) > 1 And post_combo_equip(member, 0) > 0 Then
                    eq = post_combo_equip(member, 0)
                    equip(x) = eq
                    Dim temp() As Integer = GetEquipment(eq)
                    armor_equipped = temp(0)
                    weapon_element = temp(1)
                    weapon_offense = temp(2)
                    weapon_crush = temp(3)
                    weapon_factor = temp(4) / 2
                    durability = post_combo_equip(member, 1)
                    If durability = 0 And Not armor_equipped Then
                        full_turn_weapon = True
                    Else
                        full_turn_weapon = False
                    End If
                End If
            ElseIf x > 0 Then
                equip(x) = equip(x - 1)
            End If
            eq = equip(x)

            'check for EX combos in the current turn
            Dim turn As Integer = -1
            If x = 0 OrElse member <> Me.member(x - 1) - 1 Then
                turn = Array.IndexOf(first_card, x, 0, turns)
            ElseIf Not IsAttack(combo(x - 1).Tag) Then
                turn = Array.IndexOf(first_card, x - 1, 0, turns)
            End If
            If turn <> -1 Then
                CheckCombo(turn)
            End If

            offense = LevelToOffense(level(member))
            attack = GetAttack(x)

            Dim skip_extra_hit As Boolean

            'step through current attack hit by hit
            For y = 1 To attack_data(attack, 0)

                'Strong Attack ** charge animation takes away one durability point
                If attack = 29 And y = 1 And durability > 0 And IsWeapon(eq) Then
                    durability -= 1
                End If

                skip_extra_hit = False
                If secondary_target.Checked Then
                    If secondary_target_defeated Then                 'skip every hit if secondary target is already dead
                        Continue For
                    End If
                    If Not multi_target_attacks.Contains(id) Then     'if secondary target, skip single-target attacks 
                        skip_extra_hit = True
                        Continue For
                    End If
                    If id = 28 And y < 3 Then                         'if secondary target, skip first 2 hits of Open Your Eyes
                        Continue For
                    End If
                End If

                'some attacks miss airborne targets
                If airborne_enemies.Contains(combo_target) And true_HP > 0 Then
                    If attack = 21 And y = 3 Then
                        Continue For        'Milly's Strong Attack B 3rd hit
                    End If
                    If attack = 25 Then
                        Continue For        'Rabbit Dash
                    End If
                    If attack = 31 And y = 1 Then
                        Continue For        'Milly's Medium Attack B ** 1st hit
                    End If
                    If attack = 32 And y = 3 Then
                        Continue For        'Rabbit Dash * 3rd hit
                    End If
                    If attack = 33 And y < 4 And (Not down.Checked Or My.Settings.SecretQueenGetUp) Then
                        Continue For        'Rabbit Dash ** (Secret Queen) first 3 hits if enemy isn't down
                    End If
                End If

                'random hits from Heavenlapse
                If id = 38 AndAlso My.Settings.Heavenlapse.ElementAt(y - 1) = "0" Then
                    Continue For
                End If

                'random hits from Aphelion Dustwake
                If id = 44 AndAlso My.Settings.AphelionDustwake.ElementAt(y - 1) = "0" Then
                    Continue For
                End If

                'Sagi's and Milly's attacks miss the Marauder cockpit
                If combo_target = 123 And member + 1 <> 3 Then
                    If Not (((id = 14 Or id = 17) And y = 2) Or id = 18 Or (id = 19 And y = 7) Or id = 8) Then
                        'exceptions: Ascension 2nd hit, Transcension 2nd hit, Blast Tooth, Rime Blade 7th hit, Pegasus Jump
                        Continue For
                    End If
                End If

                knockdown_hit(hits + 1) = False
                ShowHitModifiers()

                'if variable element, change attack element to match weapon
                If attack_data(attack, 27) = 6 Then
                    If Not (id = 38 AndAlso y > first_heavenlapse_hit) Then       'Heavenlapse can't change element mid-attack
                        If (durability > 0 Or full_turn_weapon) And Not armor_equipped Then
                            attack_element = weapon_element
                        Else
                            attack_element = 0
                        End If
                    End If
                Else
                    attack_element = attack_data(attack, 27)
                End If
                hit_element(hits + 1) = attack_element

                attack_offense = attack_data(attack, y)
                attack_crush = attack_data(attack, y + 13)
                offense_deviation = DeviationToNumber(hits + 1, 0)
                crush_deviation = DeviationToNumber(hits + 1, 1)

                'attack boost only lasts two turns - in case the same character attacks again in the same combo
                Select Case turns_per_member(member)
                    Case 1
                        attack_boost_factor = 1 + offense_boost(member, attack_element, 0)
                    Case 2
                        attack_boost_factor = 1 + offense_boost(member, attack_element, 1)
                    Case > 2
                        attack_boost_factor = 1
                End Select

                'lightning yields a 20% bonus if Electric Helm or Blitz Helm is equipped  
                If (eq = 300 Or eq = 315) And attack_element = 3 Then
                    armor_factor = 1.2
                Else
                    armor_factor = 1
                End If

                'element compatibility and weapon crits
                If durability > 0 Or full_turn_weapon Then
                    If Not armor_equipped Then
                        element_compatibility = Me.element_compatibility(weapon_element, attack_data(attack, 27))
                        If eq = 75 Then                 'Cutthroat Knife, 50% chance of critical hits
                            If weapon_crit(hits + 1) Then
                                weapon_factor = 1.5
                            Else
                                weapon_factor = 1
                            End If
                        End If
                    ElseIf eq = 300 Or eq = 315 Then    'Electric Helm (+10 offense), Blitz Helm (+20 offense)
                        If attack_data(attack, 27) = 6 Then
                            element_compatibility = 0.9
                        Else
                            element_compatibility = Me.element_compatibility(3, attack_data(attack, 27))
                        End If
                    End If
                Else
                    weapon_offense = 0
                    weapon_crush = 0
                    weapon_element = 0
                    element_compatibility = 0
                    weapon_factor = 0
                End If

                qm_bonus = QM_total_bonus(attack_element)
                aura_offense = Me.aura_offense(member, attack_element)
                aura_crush = Me.aura_crush(member, attack_element)

                If EX(x) > 0 Then
                    'some of Guillo's EX combos are stronger in the English version
                    If My.Settings.EnglishVersion AndAlso english_EX_combos.Contains(EX(x)) Then
                        Dim index As Integer = Array.IndexOf(english_EX_combos, EX(x))
                        ex_offense_factor = english_EX_combo_bonus(index) * 0.01 + 1
                    Else
                        ex_offense_factor = EX_combo_data(EX(x), 8) * 0.01 + 1
                    End If
                    ex_crush_factor = EX_combo_data(EX(x), 9) * 0.01 + 1
                Else
                    ex_offense_factor = 1
                    ex_crush_factor = 1
                End If

                Dim crit_bonus As Integer = 0

                'down critical hits
                If hits = 0 And down.Checked Then
                    crit_bonus += 50
                End If

                'status effect critical hits (flames -> ice, frozen -> fire, blind -> light)
                If status_crit(enemy_status, attack_element) Then
                    crit_bonus += 100
                    reset_status = True
                Else
                    reset_status = False
                End If

                'The Godling's Rapture critical hits
                If attack = 15 Then
                    'Razer 3 (final phase), Promachina Heughes 2 (final phase), Marauder 2 (final phase), Promachina Shanath (final phase), Marauder 2 cannons, Machinanguis A and B
                    If ((combo_target = 117 Or combo_target = 119 Or combo_target = 121 Or combo_target = 133) And final_phase.Checked) Or (combo_target = 122 Or combo_target = 138 Or combo_target = 139) Then
                        crit_bonus += 50
                    End If
                End If

                'Secret Queen critical hits (usually all 4 hits)
                If attack = 33 Then
                    If y > 1 Then
                        If y = 4 And hits = 0 Then      'hits = 0 means the target is airborne, so the first 3 hits were skipped
                            crit_bonus = 0              'in which case the final hit gets no bonus
                        Else
                            crit_bonus += 50
                        End If
                    ElseIf My.Settings.SecretQueenGetUp Then
                        crit_bonus = 0                  'the first hit isn't a critical hit if the enemy gets up just in time
                    End If
                End If

                'quest magnus critical hits
                If QM_total_bonus(7) = 100 Or qm_crit(hits + 1) Then
                    crit_bonus += 20
                End If

                crit_factor = crit_bonus * 0.01 + 1

                'if the enemy has a shield, breaking it reduces their defenses and their resilience to knockdown and knockout by 20%
                'the shield breaks when the enemy's (effective) HP drops below 50%
                If Not shield.Visible OrElse (shield.Checked And true_HP >= shield_limit) Then
                    base_defense = defense(combo_target, attack_element)
                    crush_limit = Me.crush_limit(combo_target)
                    knockdown = Me.knockdown(combo_target)
                    knockout = Me.knockout(combo_target)
                Else
                    base_defense = Math.Floor(defense(combo_target, attack_element) * 0.8)
                    crush_limit = Math.Floor(Me.crush_limit(combo_target) * 0.8)
                    knockdown = Math.Floor(Me.knockdown(combo_target) * 0.8)
                    knockout = Math.Floor(Me.knockout(combo_target) * 0.8)
                End If
                defense_deviation = DeviationToNumber(hits + 1, 2)
                defense_boost_factor = 1 + defense_boost(attack_element, 0)

                'calculation of totals
                total_offense = (offense * attack_offense * attack_boost_factor * (1 + offense_deviation * 0.01) * armor_factor + weapon_offense * element_compatibility * weapon_factor + qm_bonus + aura_offense) * ex_offense_factor * crit_factor
                total_crush = (offense * attack_crush * attack_boost_factor * (1 + crush_deviation * 0.01) * armor_factor + weapon_crush * element_compatibility * weapon_factor + qm_bonus + aura_crush) * ex_crush_factor * crit_factor
                total_defense = base_defense * Math.Max(0, 1 - crush_status / crush_limit) * defense_boost_factor * (1 + defense_deviation * 0.01)

                'before Sagi's awakening, any damage output on machina armas is reduced by 90%
                If ((combo_target >= 115 And combo_target <= 121) Or combo_target = 133) And Not (final_phase.Visible And final_phase.Checked) Then
                    multiplier = 0.1
                ElseIf combo_target = 126 And member + 1 = 3 Then     'when Guillo attacks a Sandfeeder, the damage output is reduced by 80%
                    multiplier = 0.2
                Else
                    multiplier = 1
                End If

                If armor_durability > 0 Then
                    armor_defense = Me.armor_defense(attack_element)
                ElseIf combo_target = 117 AndAlso (final_phase.Checked And shield.Checked And true_HP >= shield_limit And attack <> 15) Then
                    armor_defense = Me.armor_defense(attack_element)        'Razer 3 (final phase) has armor equipped until its shield breaks. The Godling's Rapture bypasses the armor
                Else
                    armor_defense = 0
                End If

                'damage output
                If total_offense > total_defense + armor_defense Then
                    damage_output = Math.Floor(Math.Round((total_offense - total_defense * 0.775 - armor_defense) * multiplier, 4, MidpointRounding.AwayFromZero))
                Else
                    damage_output = Math.Floor(Math.Round((total_offense - total_defense * 0.1 - armor_defense) * 0.25 * multiplier, 4, MidpointRounding.AwayFromZero))
                End If
                If min_one(hits + 1) And armor_defense = 0 And multiplier = 1 Then
                    damage_output = Math.Max(1, damage_output)
                Else
                    damage_output = Math.Max(0, damage_output)
                End If
                total_damage += damage_output

                'crush output
                If total_crush > total_defense * 0.5 + armor_defense Then
                    crush_output = Math.Max(0, (total_crush - total_defense * 0.3875 - armor_defense) * multiplier)
                Else
                    crush_output = Math.Max(0, (total_crush - total_defense * 0.05 - armor_defense) * 0.25 * multiplier)
                End If

                'Rabbit Dash 2nd hit knockdown
                If attack = 25 And y = 2 Then
                    crush_output = Math.Max(crush_output, knockdown - crush_status)
                End If

                Dim hp_prev As Integer = true_HP

                true_HP = Math.Max(0, true_HP - damage_output)
                effective_HP = Math.Max(0, effective_HP - damage_output)

                If shield.Visible And shield.Checked And shield_break_hit = 0 Then
                    If true_HP < shield_limit And hp_prev >= shield_limit Then
                        shield_break_hit = hits + 1
                    End If
                End If

                hits += 1

                If My.Settings.EffectiveHPRemaining Then
                    HP_remaining = effective_HP
                Else
                    HP_remaining = true_HP
                End If

                'status effect induced by a weapon
                effect_element = 0
                If weapon_effect(hits) AndAlso (durability > 0 Or full_turn_weapon) Then
                    If flames_weapons.Contains(eq) And resistance(combo_target, 0) > 0 Then
                        effect_element = 1
                    ElseIf freeze_weapons.Contains(eq) And resistance(combo_target, 1) > 0 Then
                        effect_element = 2
                    ElseIf shock_weapons.Contains(eq) And resistance(combo_target, 2) > 0 Then
                        effect_element = 3
                    ElseIf blind_weapons.Contains(eq) And resistance(combo_target, 3) > 0 Then
                        effect_element = 5
                    End If
                End If

                'two-turn offense/defense reduction triggered by a weapon
                boost_element = -1
                If weapon_boost(hits) AndAlso (durability > 0 Or full_turn_weapon) Then
                    Dim result As Double
                    If eq = 142 Then      'Rosevine: physical offense -40%
                        boost_element = 6
                        result = Round(enemy_offense_boost(0, 0) - 0.4)
                        enemy_offense_boost(0, 0) = LimitBoost(result)
                        result = Round(enemy_offense_boost(0, 1) - 0.4)
                        enemy_offense_boost(0, 1) = LimitBoost(result)
                    Else
                        Dim boost_index As Integer = Array.IndexOf(boost_weapons, eq)
                        If boost_index >= 0 Then
                            boost_element = boost_weapon_element(boost_index)
                            result = Round(defense_boost(boost_element, 0) - boost_weapon_bonus(boost_index) * 0.01)
                            defense_boost(boost_element, 0) = LimitBoost(result)
                            result = Round(defense_boost(boost_element, 1) - boost_weapon_bonus(boost_index) * 0.01)
                            defense_boost(boost_element, 1) = LimitBoost(result)
                        End If
                    End If
                End If

                hit_card(hits) = x          'an array that stores which card each hit originates from

                ShowHit(x, offense, attack_offense, attack_crush, attack_boost_factor, armor_factor, weapon_offense, effect_element, weapon_crush, boost_element, element_compatibility, weapon_factor, qm_bonus, aura_offense, aura_crush, ex_offense_factor, ex_crush_factor, crit_factor, enemy_status, base_defense, crush_limit, crush_status, defense_boost_factor, total_offense, total_crush, total_defense, multiplier, armor_defense, damage_output, crush_output, total_damage, HP_remaining, attack_element, knockdown, knockout)

                'status changes
                If reset_status Then
                    enemy_status = 0
                End If
                If effect_element > 0 Then
                    enemy_status = effect_element
                    If effect_element = 5 Then
                        enemy_status = 4
                    End If
                End If
                crush_status += crush_output
                If durability > 0 And Not full_turn_weapon And Not armor_equipped Then
                    durability -= 1
                End If
                If armor_durability > 0 Then
                    armor_durability -= 1
                End If
            Next
            first_hit(x + 1) = hits + 1     'an array that stores the first hit of each card

            'secondary target: no extra hits from single-target attacks
            If skip_extra_hit Then
                Continue For
            End If
            'skip extra hits if secondary target is already dead
            If secondary_target.Checked And secondary_target_defeated Then
                Continue For
            End If

            'extra hit when a turn ends or when using Arabesque on secondary targets
            If Me.member(x + 1) <> member + 1 Or (secondary_target.Checked And id = 24) Then
                '  ((fire attack           and no flames immunity             ) or darkness attack      ) and ((regular knockdown                          ) or (knockout                                 ) or (shock knockdown                   ) or death      )
                If ((hit_element(hits) = 1 And resistance(combo_target, 0) > 0) Or hit_element(hits) = 5) And ((crush_status >= knockdown And knockdown > 0) Or (crush_status >= knockout And knockout > 0) Or (enemy_status = 3 And knockdown > 0) Or true_HP = 0) Then
                    knockdown_hit(hits + 1) = True
                    ShowHitModifiers()

                    attack_element = hit_element(hits)
                    Select Case member + 1
                        Case 1                                              'Sagi
                            offense = Math.Round(LevelToOffense(level(0)) * 10, MidpointRounding.AwayFromZero)
                        Case 2                                              'Milly
                            offense = Math.Round(LevelToOffense(level(1)) * 7, MidpointRounding.AwayFromZero)
                        Case 3                                              'Guillo
                            offense = Math.Round(LevelToOffense(level(2)) * 15, MidpointRounding.AwayFromZero)
                    End Select
                    offense_deviation = DeviationToNumber(hits + 1, 0)
                    qm_bonus = QM_total_bonus(6)

                    'max HP is "defense" since it serves as a limit to the damage output
                    base_defense = HP(combo_target)
                    defense_deviation = DeviationToNumber(hits + 1, 2)

                    total_offense = (offense + qm_bonus) * (1 + offense_deviation * 0.01)
                    total_defense = base_defense * 0.1 * (1 + defense_deviation * 0.01)

                    damage_output = Math.Floor(Math.Min(total_offense, total_defense))
                    total_damage += damage_output

                    Dim hp_prev As Integer = true_HP

                    true_HP = Math.Max(0, true_HP - damage_output)
                    effective_HP = Math.Max(0, effective_HP - damage_output)

                    If shield.Visible And shield.Checked And shield_break_hit = 0 Then
                        If true_HP < shield_limit And hp_prev >= shield_limit Then
                            shield_break_hit = hits + 1
                        End If
                    End If

                    hits += 1

                    If Not My.Settings.EffectiveHPRemaining Then
                        HP_remaining = true_HP
                    Else
                        HP_remaining = effective_HP
                    End If

                    hit_card(hits) = x

                    ShowKnockdownHit(offense, qm_bonus, enemy_status, base_defense, total_offense, total_defense, damage_output, total_damage, HP_remaining, attack_element)

                    first_hit(x + 1) += 1
                End If

                'Firedrake Regalia / Aetherdrake Regalia
                If eq = 238 Or eq = 249 Then
                    knockdown_hit(hits + 1) = False
                    ShowHitModifiers()

                    'enemy offense is used instead of Milly's offense
                    offense = enemy_offense(combo_target)
                    attack_boost_factor = 1 + enemy_offense_boost(0, 0)
                    offense_deviation = DeviationToNumber(hits + 1, 0)

                    If Not shield.Visible OrElse (shield.Checked And true_HP >= shield_limit) Then
                        base_defense = defense(combo_target, attack_element)
                        crush_limit = Me.crush_limit(combo_target)
                        knockdown = Me.knockdown(combo_target)
                        knockout = Me.knockout(combo_target)
                    Else
                        base_defense = Math.Floor(defense(combo_target, attack_element) * 0.8)
                        crush_limit = Math.Floor(Me.crush_limit(combo_target) * 0.8)
                        knockdown = Math.Floor(Me.knockdown(combo_target) * 0.8)
                        knockout = Math.Floor(Me.knockout(combo_target) * 0.8)
                    End If

                    defense_deviation = DeviationToNumber(hits + 1, 2)
                    defense_boost_factor = 1 + defense_boost(attack_element, 0)

                    'before Sagi's awakening, any damage output on Machina Armas is reduced by 90%
                    If ((combo_target >= 115 And combo_target <= 121) Or combo_target = 133) And Not (final_phase.Visible And final_phase.Checked) Then
                        multiplier = 0.1
                    ElseIf combo_target = 126 And member + 1 = 3 Then     'when Guillo attacks a Sandfeeder, the damage output is reduced by 80%
                        multiplier = 0.2
                    Else
                        multiplier = 1
                    End If

                    'calculation of totals
                    total_offense = offense * 50 * attack_boost_factor * (1 + offense_deviation * 0.01)
                    total_defense = base_defense * Math.Max(0, 1 - crush_status / crush_limit) * defense_boost_factor * (1 + defense_deviation * 0.01)

                    If armor_durability > 0 Then
                        armor_defense = Me.armor_defense(attack_element)
                    ElseIf combo_target = 117 AndAlso (final_phase.Checked And shield.Checked And true_HP >= shield_limit) Then
                        armor_defense = Me.armor_defense(attack_element)        'Razer 3 (final phase) has armor equipped until its shield breaks
                    Else
                        armor_defense = 0
                    End If

                    'damage output
                    If total_offense > total_defense + armor_defense Then
                        damage_output = Math.Floor(Math.Round((total_offense - total_defense * 0.775 - armor_defense) * multiplier, 4, MidpointRounding.AwayFromZero))
                    Else
                        damage_output = Math.Floor(Math.Round((total_offense - total_defense * 0.1 - armor_defense) * 0.25 * multiplier, 4, MidpointRounding.AwayFromZero))
                    End If
                    If min_one(hits + 1) And armor_defense = 0 And multiplier = 1 Then
                        damage_output = Math.Max(1, damage_output)
                    Else
                        damage_output = Math.Max(0, damage_output)
                    End If
                    total_damage += damage_output
                    crush_output = 0

                    Dim hp_prev As Integer = true_HP

                    true_HP = Math.Max(0, true_HP - damage_output)
                    effective_HP = Math.Max(0, effective_HP - damage_output)

                    If shield.Visible And shield.Checked And shield_break_hit = 0 Then
                        If true_HP < shield_limit And hp_prev >= shield_limit Then
                            shield_break_hit = hits + 1
                        End If
                    End If

                    hits += 1

                    If Not My.Settings.EffectiveHPRemaining Then
                        HP_remaining = true_HP
                    Else
                        HP_remaining = effective_HP
                    End If

                    hit_card(hits) = x

                    ShowRegaliaHit(offense, attack_boost_factor, enemy_status, base_defense, crush_limit, defense_boost_factor, multiplier, total_offense, crush_status, total_defense, armor_defense, damage_output, total_damage, HP_remaining)

                    first_hit(x + 1) += 1
                End If
            End If
        Next

        'save stats for next combo
        If cards > 0 Then
            post_combo_HP = true_HP
            post_combo_armor = armor_durability
            If shield.Visible And (Not shield.Checked Or true_HP < shield_limit) Then
                post_combo_shield = False
            Else
                post_combo_shield = True
            End If

            'knockdown
            If (crush_status >= knockdown And knockdown > 0) OrElse (crush_status >= knockout And knockout > 0) Then
                post_combo_down = True
            ElseIf enemy_status = 3 And knockdown > 0 And (hits > 1 OrElse (hits = 1 AndAlso table(1, 2).Text <> "")) Then
                post_combo_down = True    'shock
            Else
                post_combo_down = False
            End If

            'status effect
            post_combo_status = enemy_status

            'equipment after final card
            Dim final_member As Integer = Me.member(cards - 1) - 1
            If durability > 0 Or armor_equipped Or IsWeapon(combo(cards - 1).Tag) Then
                post_combo_equip(final_member, 0) = equip(cards - 1)
                post_combo_equip(final_member, 1) = durability
            Else
                post_combo_equip(final_member, 0) = 0
                post_combo_equip(final_member, 1) = 0
            End If

            'equipment from characters who didn't participate in the combo
            For x = 0 To 2
                If turns_per_member(x) = 0 Then
                    post_combo_equip(x, 0) = equipment(x).Tag
                    If eq_durability(x).Text <> "" Then
                        post_combo_equip(x, 1) = eq_durability(x).Text
                    End If
                End If
            Next

            'offense boost only lasts two turns
            For x = 0 To 2
                If turns_per_member(x) = 0 Then
                    For y = 0 To 5
                        post_combo_offense_boost(x, y, 0) = offense_boost(x, y, 0)
                        post_combo_offense_boost(x, y, 1) = offense_boost(x, y, 1)
                    Next
                ElseIf turns_per_member(x) = 1 Then
                    For y = 0 To 5
                        post_combo_offense_boost(x, y, 0) = offense_boost(x, y, 1)
                        post_combo_offense_boost(x, y, 1) = 0
                    Next
                Else
                    For y = 0 To 5
                        post_combo_offense_boost(x, y, 0) = 0
                        post_combo_offense_boost(x, y, 1) = 0
                    Next
                End If
            Next

            'one combo (relay or not) counts as one defense turn to the enemy
            If hits > 0 Then
                For x = 0 To 5
                    post_combo_enemy_defense_boost(x, 0) = defense_boost(x, 1)
                    post_combo_enemy_defense_boost(x, 1) = 0
                Next
            End If

            For x = 0 To 5
                post_combo_enemy_offense_boost(x, 0) = enemy_offense_boost(x, 0)
                post_combo_enemy_offense_boost(x, 1) = enemy_offense_boost(x, 1)
            Next

            next_combo.Enabled = True
        Else
            next_combo.Enabled = False
        End If

        Clear(hits_prev)    'clear data beyond the current combo

        For x = 1 To hits
            For y = 0 To 2
                AddHandler hit_modifier(x, y).SelectedIndexChanged, AddressOf ChangeDeviation
            Next
        Next

        ShowComboResults(total_damage)
    End Sub

    Private Sub ShowComboResults(total_damage As Integer)
        Dim combo_cards As Integer = cards
        Dim final_cards As String = ""
        Dim final_hits As String = ""
        Dim final_damage As String = ""
        Dim TP_bonus As String = ""
        If cards > 0 AndAlso relay_equip Then
            combo_cards -= 1
        End If
        If combo_cards > 1 Then
            If hits > 1 Then
                final_cards = combo_cards & " cards, "
            Else
                final_cards = combo_cards & " cards"
            End If
        End If
        If hits > 1 Then
            final_hits = hits & " hits"
        ElseIf combo_cards = 2 Then
            final_cards = ""
        End If
        If hits > 0 Then
            final_damage = total_damage & " damage"
        End If
        If combo_cards > 2 Then
            TP_bonus = "+" & Math.Pow(combo_cards - 2, 3) & " TP bonus"
        End If
        combo_results.Text = final_cards & final_hits & vbCrLf & final_damage & vbCrLf & TP_bonus
    End Sub

    Private Sub ChangeMP(card As Integer)
        If burst_active Then
            dummy.Left = 92 + cards * 50 + card_panel(1).AutoScrollPosition.X
            burst.Hide()
            burst.Left = 19 + cards * 50 + card_panel(1).AutoScrollPosition.X
            burst.Show()
            Return
        End If

        Dim id As Integer
        Dim delta As Double
        Dim value As Double = current_MP
        If card = cards - 1 Then                                        'add card
            id = combo(cards - 1).Tag
            delta = MP.factor * MP_gain(cards - 1) - 100 * MP_cost(id)
            value = Round(value + delta)
            value = Math.Min(value, MP.max_MP)
        Else
            For x = card To cards Step -1                               'remove card(s)
                id = combo(x).Tag
                delta = MP.factor * MP_gain(x) - 100 * MP_cost(id)
                value = Round(value - delta)
                value = Clamp(value, 0, MP.max_MP)
            Next
        End If

        MP.MP.Text = value
        DisplayMP(value)
    End Sub

    Public Sub DisplayMP(new_MP As Double)
        current_MP = new_MP
        If enemy_mode Then
            Return
        End If

        dummy.Left = 92 + cards * 50 + card_panel(1).AutoScrollPosition.X
        dummy.Show()
        MP_display.Hide()
        burst.Hide()
        MP_display.Text = new_MP
        If MP.Visible Then
            If new_MP < 500 Then
                MP_display.Left = 25 + cards * 50 + card_panel(1).AutoScrollPosition.X
                MP_display.Show()
            Else
                If burst_active Then
                    burst.Text = "Cancel"
                    burst.ForeColor = Color.Red
                Else
                    burst.Text = "Burst"
                    burst.ForeColor = Color.Black
                End If
                burst.Left = 19 + cards * 50 + card_panel(1).AutoScrollPosition.X
                burst.Show()
            End If
        Else
            dummy.Hide()
        End If
        CheckCards()
    End Sub

    Private Sub ToggleBurst()
        If burst_active Then
            MP.MP.Text = "0"
            DisplayMP(0)
            MP.burst.Checked = False
        Else
            MP.burst.Checked = True
        End If
        card_panel(1).Focus()
    End Sub

    Public Sub E_Calculate()
        If locked Then
            Return
        End If

        Dim chr, attack_element, bitfield, prev_bitfield, offense_deviation, crush_deviation, armor_offense, base_defense, qm_defense, defense_deviation, aura_defense, armor_defense, armor_durability, damage_output, total_damage, HP_remaining, max_HP As Integer
        Dim crush, offense, attack_offense, attack_crush, attack_boost_factor, crit_factor, crush_limit, crush_status, defense_boost_factor, armor_factor, total_offense, total_crush, total_defense, crush_output, defense_boost(6, 2), knockdown, knockout As Double
        Dim reset_status, skip_extra_hit, full_turn_armor, armor_broken As Boolean

        chr = character - 1
        Dim hits_prev As Integer = hits
        For x = 1 To hits_prev
            For y = 0 To 2
                RemoveHandler E_hit_modifier(x, y).SelectedIndexChanged, AddressOf ChangeDeviation
            Next
        Next
        hits = 0
        HP_remaining = member_hp
        max_HP = member_max_hp_armor
        crush_status = 0
        Dim status As Integer = E_status.SelectedIndex
        For x = 0 To 5
            For y = 0 To 1
                defense_boost(x, y) = Me.defense_boost(chr, x, y)
            Next
        Next

        Dim eq As Integer = equipment(chr).Tag
        If IsArmor(eq) OrElse IsAccessory(eq) Then
            If eq_durability(chr).Text <> "" Then
                armor_durability = eq_durability(chr).Text
            Else
                armor_durability = 0
            End If
            If armor_durability = 0 Then
                full_turn_armor = True
            End If
        End If

        If combo_target = 117 And E_armor.Checked Then
            armor_offense = 20
        End If

        'step through combo attack by attack
        For x = 0 To cards - 1
            Dim id As Integer = combo(x).Tag
            offense = enemy_offense(combo_target)
            crush = enemy_crush(combo_target)
            Dim number_of_hits As Integer = enemy_attack_data(combo_target, id, 1)

            'step through current attack hit by hit
            For y = 0 To number_of_hits - 1
                If hits > 0 And eq <> 0 Then
                    If armor_durability = 0 And Not full_turn_armor Then
                        armor_broken = True
                    End If
                End If

                attack_element = enemy_attack_data(combo_target, id, 2 + y * 4)
                prev_bitfield = bitfield
                bitfield = enemy_attack_data(combo_target, id, 3 + y * 4)

                skip_extra_hit = False
                If E_secondary_target.Checked Then
                    Dim multi As Boolean
                    For bit = 5 To 7
                        If Math.Floor(bitfield / Math.Pow(2, bit)) Mod 2 = 1 Then
                            multi = True
                            Exit For
                        End If
                    Next
                    If Not multi Then           'if secondary target, skip single-target attacks
                        skip_extra_hit = True
                        Continue For
                    End If
                End If

                If (combo_target = 124 Or combo_target = 125) AndAlso id = 3 Then       'Godcraft, Seginus
                    If My.Settings.Heavenlapse.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                If combo_target = 90 AndAlso id = 3 Then                                'High-Mobility Cannon
                    If My.Settings.Fusillade.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                If combo_target = 134 AndAlso id = 3 Then                               'Black Dragon
                    If My.Settings.CrimsonCatharsis.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                If (combo_target = 138 Or combo_target = 139) AndAlso id = 3 Then       'Machinanguis A/B
                    If My.Settings.BerserkTech.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                If combo_target = 139 AndAlso id = 4 Then                               'Machinanguis B
                    If My.Settings.HyperslaughtMode4.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                If combo_target = 139 AndAlso id = 5 Then                               'Machinanguis B
                    If My.Settings.HyperslaughtMode3.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                If combo_target = 140 AndAlso id = 1 Then                               'true final boss
                    If My.Settings.MagnusOfLife.ElementAt(y) = "0" Then
                        Continue For
                    End If
                End If

                knockdown_hit(hits + 1) = False
                E_ShowHitModifiers()
                hit_element(hits + 1) = attack_element

                Dim prima_queen_factor As Double = 1
                If combo_target = 70 And id = 3 Then
                    prima_queen_factor += 0.75 * E_prima_queens.SelectedIndex
                End If

                attack_offense = enemy_attack_data(combo_target, id, 4 + y * 4) * prima_queen_factor
                attack_crush = enemy_attack_data(combo_target, id, 5 + y * 4) * prima_queen_factor
                offense_deviation = E_DeviationToNumber(hits + 1, 0)
                crush_deviation = E_DeviationToNumber(hits + 1, 1)
                attack_boost_factor = 1 + enemy_offense_boost(attack_element, 0)

                Dim crit_bonus As Integer = 0

                'down critical hits
                If hits = 0 And E_down.Checked Then
                    crit_bonus += 50
                End If

                'mid-combo knockdown (Lunchtime, Death by Stomping)
                If hits > 0 AndAlso Math.Floor(prev_bitfield / Math.Pow(2, 13)) Mod 2 = 1 Then
                    crit_bonus += 50
                End If

                'status effect critical hits (flames -> ice, frozen -> fire, blind -> light)
                If status_crit(status, attack_element) Then
                    crit_bonus += 100
                    reset_status = True
                Else
                    reset_status = False
                End If

                crit_factor = crit_bonus * 0.01 + 1

                If eq = 243 And armor_durability > 0 And prevent_crit(hits + 1) Then    'Jiraiya's Robe blocks critical hits (80% chance)
                    crit_factor = 1
                End If

                If eq = 254 And armor_durability > 0 Then                               'Saizou's Robe blocks all critical hits
                    crit_factor = 1
                End If

                base_defense = member_defense(attack_element)
                If attack_element = 0 Then
                    qm_defense = QM_total_bonus(9)
                Else
                    qm_defense = 0
                End If

                Dim crush_limit_bonus As Integer
                If armor_durability > 0 Or full_turn_armor Then
                    crush_limit_bonus = armor_crush_limit(eq)
                    knockdown = member_knockdown_armor
                    knockout = member_knockout_armor
                    armor_defense = magnus_defense(eq, attack_element)
                Else
                    crush_limit_bonus = 0
                    knockdown = member_knockdown
                    knockout = member_knockout
                    armor_defense = 0
                End If

                crush_limit = member_crush_limit * (1 + 0.01 * crush_limit_bonus) + aura_crush_limit(chr)
                defense_boost_factor = 1 + defense_boost(attack_element, 0)
                defense_deviation = E_DeviationToNumber(hits + 1, 2)

                armor_factor = 1
                If eq = 431 And armor_durability > 0 Then           'Vanishing Cloak reduces defense by 50%
                    armor_factor = 0.5
                End If

                aura_defense = Me.aura_defense(chr, attack_element)

                'calculation of totals
                total_offense = (offense * attack_offense * attack_boost_factor * (1 + offense_deviation * 0.01) + armor_offense) * crit_factor
                total_crush = (crush * attack_crush * attack_boost_factor * (1 + crush_deviation * 0.01) + armor_offense) * crit_factor
                total_defense = (base_defense + qm_defense) * Math.Max(0, 1 - crush_status / crush_limit) * defense_boost_factor * (1 + defense_deviation * 0.01) * armor_factor + aura_defense

                'damage output
                If total_offense > total_defense + armor_defense Then
                    damage_output = Math.Floor(Math.Round(total_offense - total_defense * 0.775 - armor_defense, 4, MidpointRounding.AwayFromZero))
                Else
                    damage_output = Math.Floor(Math.Round((total_offense - total_defense * 0.1 - armor_defense) * 0.25, 4, MidpointRounding.AwayFromZero))
                End If
                If min_one(hits + 1) Then
                    damage_output = Math.Max(1, damage_output)
                Else
                    damage_output = Math.Max(0, damage_output)
                End If

                'crush output
                If total_crush > total_defense * 0.5 + armor_defense Then
                    crush_output = Math.Max(0, total_crush - total_defense * 0.3875 - armor_defense)
                Else
                    crush_output = Math.Max(0, (total_crush - total_defense * 0.05 - armor_defense) * 0.25)
                End If

                If armor_durability > 0 Then
                    If eq = 242 Or eq = 253 Or eq = 430 Then                                'Heavenbolt Wrap, Heavengale Wrap, and Imperial Ward block all hits
                        damage_output = 0
                        crush_output = 0
                    End If
                    If (eq = 239 Or eq = 250) And attack_element = 1 Then                   'Heat Camouflage and Hazyfire Camouflage blocks fire hits
                        damage_output = 0
                        crush_output = 0
                    End If
                    If (eq = 241 Or eq = 252) And attack_element = 2 Then                   'Aqua Camouflage and Hazyrain Camouflage blocks ice hits
                        damage_output = 0
                        crush_output = 0
                    End If
                    If (eq = 267 Or eq = 287 Or eq = 288 Or eq = 289) And hits = 0 Then     'Mephistopheles Cloak and its upgrades block the first hit
                        damage_output = 0
                        crush_output = 0
                    End If
                End If

                total_damage += damage_output
                HP_remaining = Math.Max(0, HP_remaining - damage_output)

                If eq = 256 And armor_durability = 0 Then                   'adjust HP after Confessional Clothes break
                    max_HP = member_max_hp
                    HP_remaining = Math.Min(HP_remaining, max_HP)
                End If

                hits += 1

                hit_card(hits) = x          'an array that stores which card each hit originates from

                E_ShowHit(offense, crush, attack_offense, attack_crush, attack_boost_factor, armor_offense, crit_factor, status, base_defense, qm_defense, crush_limit, crush_status, defense_boost_factor, armor_factor, aura_defense, total_offense, total_crush, total_defense, armor_defense, damage_output, crush_output, total_damage, HP_remaining, attack_element, knockdown, knockout)

                'status changes
                If reset_status Then
                    status = 0
                End If
                crush_status += crush_output

                If armor_durability > 0 Then
                    armor_durability -= 1
                End If
            Next
            first_hit(x + 1) = hits + 1     'an array that stores the first hit of each card

            'secondary target: no extra hits from single-target attacks
            If skip_extra_hit Then
                Continue For
            End If

            'extra hit on last attack
            If x + 1 = cards Then
                '  (fire attack           or darkness attack      ) and (regular knockdown         or knockout                 or shock      or death           )
                If (hit_element(hits) = 1 Or hit_element(hits) = 5) And (crush_status >= knockdown Or crush_status >= knockout Or status = 3 Or HP_remaining = 0) Then
                    knockdown_hit(hits + 1) = True
                    E_ShowHitModifiers()

                    attack_element = hit_element(hits)
                    offense = enemy_knockdown_offense(combo_target)
                    offense_deviation = E_DeviationToNumber(hits + 1, 0)

                    'max HP is "defense" since it serves as a limit to the damage output
                    base_defense = max_HP
                    defense_deviation = E_DeviationToNumber(hits + 1, 2)

                    total_offense = offense * (1 + offense_deviation * 0.01)
                    total_defense = base_defense * 0.1 * (1 + defense_deviation * 0.01)

                    damage_output = Math.Floor(Math.Min(total_offense, total_defense))
                    total_damage += damage_output

                    HP_remaining = Math.Max(0, HP_remaining - damage_output)
                    hits += 1

                    hit_card(hits) = x

                    E_ShowKnockdownHit(offense, status, base_defense, total_offense, total_defense, damage_output, total_damage, HP_remaining, attack_element)

                    first_hit(x + 1) += 1
                End If
            End If
        Next

        'save stats for next combo
        If cards > 0 Then
            post_combo_HP = HP_remaining

            'knockdown
            If crush_status >= knockdown OrElse crush_status >= knockout Then
                post_combo_down = True
            ElseIf status = 3 And hits > 0 Then
                post_combo_down = True    'shock
            Else
                post_combo_down = False
            End If

            If combo_target = 135 Or combo_target = 140 Then
                If combo(cards - 1).Tag = 2 Then        'Cast Away Your Carnal Robes overrides knockdown/knockout prevention
                    armor_broken = True
                End If
            End If

            If Not armor_broken Then
                Select Case eq
                    Case 246, 427                       'Fighter's Gi and Hermit's Cane prevent knockdown
                        If crush_status >= knockdown And crush_status < knockout Then
                            post_combo_down = False
                        End If
                    Case 247                            'Warrior's Gi prevents knockout
                        If crush_status >= knockout Then
                            post_combo_down = False
                        End If
                    Case 428                            'Warrior's Scarf prevents knockdown and knockout
                        post_combo_down = False
                End Select
            End If

            'status effect
            post_combo_status = status

            'equipment after final card
            If IsWeapon(eq) Then
                post_combo_equip(chr, 0) = eq
                If eq_durability(chr).Text = "" Then
                    post_combo_equip(chr, 1) = 0
                Else
                    post_combo_equip(chr, 1) = eq_durability(chr).Text
                End If
            Else
                If armor_durability > 0 Then
                    post_combo_equip(chr, 0) = eq
                    post_combo_equip(chr, 1) = armor_durability
                Else
                    post_combo_equip(chr, 0) = 0
                    post_combo_equip(chr, 1) = 0
                End If
            End If

            'offense boost only lasts two turns
            For x = 0 To 5
                    post_combo_enemy_offense_boost(x, 0) = enemy_offense_boost(x, 1)
                    post_combo_enemy_offense_boost(x, 1) = 0
                Next

            'one combo (relay or not) counts as one defense turn to the enemy
            If hits > 0 Then
                For x = 0 To 5
                    post_combo_defense_boost(chr, x, 0) = defense_boost(x, 1)
                    post_combo_defense_boost(chr, x, 1) = 0
                Next
            End If

            next_combo.Enabled = True
        Else
            next_combo.Enabled = False
        End If

        E_Clear(hits_prev)    'clear data beyond the current combo

        For x = 1 To hits
            For y = 0 To 2
                AddHandler E_hit_modifier(x, y).SelectedIndexChanged, AddressOf ChangeDeviation
            Next
        Next

        Dim final_hits As String = ""
        Dim final_damage As String = ""
        If hits > 1 Then
            final_hits = hits & " hits"
        End If
        If hits > 0 Then
            final_damage = total_damage & " damage"
        End If
        combo_results.Text = final_hits & vbCrLf & final_damage
    End Sub

    Public Sub ScrollToEnd()
        With card_panel(1).HorizontalScroll
            If .Visible Then
                .Value = .Maximum
                card_panel(1).PerformLayout()
            End If
        End With
    End Sub

    Public Sub ScrollToStart()
        With card_panel(0).HorizontalScroll
            If .Visible Then
                .Value = .Minimum
                card_panel(0).PerformLayout()
            End If
        End With
    End Sub

    Public Function Round(input As Double) As Double
        Return Math.Round(Math.Round(input, 12), 3, MidpointRounding.AwayFromZero)      'floating-point error mitigation while rounding to 3 decimals
    End Function

    Public Function Clamp(input As Double, min As Double, max As Double) As Double      'limit input number to a range
        Return Math.Max(min, Math.Min(input, max))
    End Function

    Private Function LimitBoost(input As Double) As Double
        Return Clamp(input, -1000, 1000)
    End Function

    Private Sub ShowHitModifiers()
        Dim hit As Integer = hits + 1

        For i = 0 To 2
            If My.Settings.ResultsRow.ElementAt(hit_modifier_row(i)) = "0" Then     'row hidden by user
                Continue For
            End If
            With hit_modifier(hit, i)
                .Top = row_pos(hit_modifier_row(i)) + output_panel.AutoScrollPosition.Y
                If Not output_panel.Contains(hit_modifier(hit, i)) Then
                    .Left = 104 + 52 * hit + output_panel.AutoScrollPosition.X
                    output_panel.Controls.Add(hit_modifier(hit, i))
                    .SelectedIndex = 3
                End If
                .Show()
            End With
        Next

        Dim offense As ComboBox = hit_modifier(hit, 0)
        Dim crush As ComboBox = hit_modifier(hit, 1)
        Dim defense As ComboBox = hit_modifier(hit, 2)

        'on fire/dark knockdown extra hits, offense/defense deviation ranges from -20% to +4%
        If knockdown_hit(hit) And offense.Items.Count < 25 Then
            offense.Items.Clear()
            defense.Items.Clear()
            For i = 4 To 1 Step -1
                offense.Items.Add("+" & i & "%")
                defense.Items.Add("+" & i & "%")
            Next
            For i = 0 To -20 Step -1
                offense.Items.Add(i & "%")
                defense.Items.Add(i & "%")
            Next
            offense.SelectedIndex = 12
            defense.SelectedIndex = 12
            crush.SelectedIndex = 3
        ElseIf Not knockdown_hit(hit) And offense.Items.Count >= 25 Then
            offense.Items.Clear()
            defense.Items.Clear()
            For i = 3 To 1 Step -1
                offense.Items.Add("+" & i & "%")
                defense.Items.Add("+" & i & "%")
            Next
            For i = 0 To -4 Step -1
                offense.Items.Add(i & "%")
                defense.Items.Add(i & "%")
            Next
            offense.SelectedIndex = 3
            defense.SelectedIndex = 3
            crush.SelectedIndex = 3
        End If
    End Sub

    Private Sub NewHit()
        For y = 0 To rows - 1
            With table(hits, y)
                If Not hit_modifier_row.Contains(y) Then
                    .Text = ""
                    If highlight = hit_card(hits) Then
                        .BackColor = Color.LightBlue
                    ElseIf .BackColor <> default_color Then
                        .BackColor = default_color
                    End If
                End If

                If My.Settings.ResultsRow.ElementAt(y) = "0" Or hit_modifier_row.Contains(y) Then
                    Continue For
                End If

                .Top = row_pos(y) + output_panel.AutoScrollPosition.Y
                If Not output_panel.Contains(table(hits, y)) Then
                    .Left = 104 + 52 * hits + output_panel.AutoScrollPosition.X
                    output_panel.Controls.Add(table(hits, y))
                End If
                .Show()
            End With
        Next
    End Sub

    Private Sub ShowRegaliaHit(offense As Double, attack_boost_factor As Double, enemy_status As Integer, base_defense As Integer, max_crush As Integer, defense_boost_factor As Double, multiplier As Double, total_offense As Double, crush_status As Double, total_defense As Double, armor_defense As Integer, damage_output As Integer, total_damage As Integer, enemy_HP As Integer)
        NewHit()

        table(hits, 0).Text = offense
        table(hits, 1).Text = 50            'Firedrake/Aetherdrake Regalia's "attack offense" is always 50
        If attack_boost_factor <> 1 Then
            table(hits, 3).Text = Round(attack_boost_factor)
        End If

        If enemy_status <> 0 Then
            If enemy_status = 4 Then
                table(hits, 16).BackColor = element_color(5)
            Else
                table(hits, 16).BackColor = element_color(enemy_status)
            End If
        End If

        table(hits, 17).Text = base_defense
        table(hits, 18).Text = max_crush
        table(hits, 19).Text = Decimals(crush_status)
        If defense_boost_factor <> 1 Then
            table(hits, 20).Text = Round(defense_boost_factor)
        End If
        table(hits, 22).Text = Decimals(total_offense)
        table(hits, 24).Text = Decimals(total_defense)
        If armor_defense <> 0 Then
            table(hits, 25).Text = armor_defense
        End If
        If multiplier <> 1 Then
            table(hits, 26).Text = multiplier
        End If
        table(hits, 27).Text = damage_output
        table(hits, 28).Text = 0
        table(hits, 29).Text = total_damage
        table(hits, 30).Text = Decimals(crush_status)
        table(hits, 31).Text = enemy_HP
        table(hits, 27).BackColor = element_color(0)
        table(hits, 28).BackColor = element_color(0)

        If hits = shield_break_hit Then
            table(hits, 31).BackColor = Color.LightPink
        End If

        Dim change_color() As Integer = {4, 5, 7, 8, 16, 21, 27, 28, 31}
        For y = 0 To rows - 1
            If Not hit_modifier_row.Contains(y) And Not change_color.Contains(y) Then
                table(hits, y).BackColor = table(hits - 1, y).BackColor
            End If
        Next
        If hits = 1 Then
            table(hits, 10).BackColor = default_color
        End If
    End Sub

    Private Sub ShowKnockdownHit(offense As Integer, qm_bonus As Integer, enemy_status As Integer, base_defense As Integer, total_offense As Double, total_defense As Double, damage_output As Integer, total_damage As Integer, enemy_hp As Integer, attack_element As Integer)
        NewHit()

        table(hits, 0).Text = offense

        If qm_bonus <> 0 Then
            table(hits, 11).Text = qm_bonus
        End If

        If enemy_status <> 0 Then
            If enemy_status = 4 Then
                table(hits, 16).BackColor = element_color(5)
            Else
                table(hits, 16).BackColor = element_color(enemy_status)
            End If
        End If

        table(hits, 17).Text = base_defense
        table(hits, 20).Text = "0.1"

        table(hits, 22).Text = Decimals(total_offense)
        table(hits, 24).Text = Decimals(total_defense)

        table(hits, 27).Text = damage_output
        table(hits, 28).Text = 0
        table(hits, 29).Text = total_damage
        table(hits, 30).Text = table(hits - 1, 30).Text
        table(hits, 31).Text = enemy_hp
        table(hits, 27).BackColor = element_color(attack_element)
        table(hits, 28).BackColor = element_color(attack_element)

        If hits = shield_break_hit Then
            table(hits, 31).BackColor = Color.LightPink
        End If

        Dim change_color() As Integer = {4, 5, 7, 8, 16, 21, 27, 28, 31}
        For y = 0 To rows - 1
            If Not hit_modifier_row.Contains(y) And Not change_color.Contains(y) Then
                table(hits, y).BackColor = table(hits - 1, y).BackColor
            End If
        Next
    End Sub

    Private Sub ShowHit(x As Integer, offense As Double, attack_offense As Double, attack_crush As Double, attack_boost_factor As Double, armor_factor As Double, weapon_offense As Integer, effect_element As Integer, weapon_crush As Integer, boost_element As Integer, element_compatibility As Double, weapon_factor As Double, qm_bonus As Integer, aura_offense As Integer, aura_crush As Integer, ex_offense_factor As Double, ex_crush_factor As Double, crit_factor As Double, enemy_status As Integer, base_defense As Integer, max_crush As Integer, crush_status As Double, defense_boost_factor As Double, total_offense As Double, total_crush As Double, total_defense As Double, multiplier As Double, armor_defense As Integer, damage_output As Integer, crush_output As Double, total_damage As Integer, enemy_hp As Integer, attack_element As Integer, knock_down As Integer, knock_out As Integer)
        NewHit()

        table(hits, 0).Text = offense
        table(hits, 1).Text = attack_offense
        table(hits, 2).Text = attack_crush
        If attack_boost_factor <> 1 Then
            table(hits, 3).Text = Round(attack_boost_factor)
        End If
        If armor_factor <> 1 Then
            table(hits, 6).Text = armor_factor
        End If

        If weapon_offense <> 0 Or weapon_crush <> 0 Then
            table(hits, 7).Text = weapon_offense
            If effect_element <> 0 Then
                table(hits, 7).BackColor = element_color(effect_element)
            End If
            table(hits, 8).Text = weapon_crush
            If boost_element <> -1 Then
                If boost_element < 6 Then
                    table(hits, 8).BackColor = element_color(boost_element)
                Else
                    table(hits, 8).BackColor = Color.Orange
                End If
            End If
            table(hits, 9).Text = element_compatibility
            If weapon_factor <> 1 Then
                table(hits, 10).Text = weapon_factor
            End If
        End If
        If qm_bonus <> 0 Then
            table(hits, 11).Text = qm_bonus
        End If
        If aura_offense <> 0 Then
            table(hits, 12).Text = aura_offense
        End If
        If aura_crush <> 0 Then
            table(hits, 13).Text = aura_crush
        End If

        If EX(x) <> 0 Then
            table(hits, 14).Text = ex_offense_factor
            table(hits, 15).Text = ex_crush_factor
        End If
        If crit_factor <> 1 Then
            table(hits, 16).Text = crit_factor
            If qm_crit(hits) And QM_total_bonus(7) < 100 Then
                table(hits, 16).Font = New Font("Segoe UI", 9, FontStyle.Bold)
            Else
                table(hits, 16).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            End If
        End If
        If enemy_status <> 0 Then
            If enemy_status = 4 Then
                table(hits, 16).BackColor = element_color(5)
            Else
                table(hits, 16).BackColor = element_color(enemy_status)
            End If
        End If

        table(hits, 17).Text = base_defense
        table(hits, 18).Text = max_crush
        table(hits, 19).Text = Decimals(crush_status)
        If defense_boost_factor <> 1 Then
            table(hits, 20).Text = Round(defense_boost_factor)
        End If

        table(hits, 22).Text = Decimals(total_offense)
        table(hits, 23).Text = Decimals(total_crush)
        table(hits, 24).Text = Decimals(total_defense)

        If armor_defense <> 0 Then
            table(hits, 25).Text = armor_defense
        End If
        If multiplier <> 1 Then
            table(hits, 26).Text = multiplier
        End If

        table(hits, 27).Text = damage_output
        table(hits, 28).Text = Decimals(crush_output)

        table(hits, 29).Text = total_damage
        Dim crush As Double = crush_status + crush_output
        table(hits, 30).Text = Decimals(crush)
        table(hits, 31).Text = enemy_hp

        If hits = shield_break_hit Then
            table(hits, 31).BackColor = Color.LightPink
        End If

        table(hits, 27).BackColor = element_color(attack_element)
        table(hits, 28).BackColor = element_color(attack_element)

        If crush >= knock_out And knock_out > 0 Then
            table(hits, 30).BackColor = Color.Orange
        ElseIf crush >= knock_down And knock_down > 0 Then
            table(hits, 30).BackColor = Color.Yellow
        End If
    End Sub

    Private Function GetAttack(index As Integer) As Integer
        Dim card As Integer = combo(index).Tag
        Select Case member(index)
            Case 1                       'Sagi
                If card < 7 Then
                    Return card - 1             'regular attacks
                Else
                    Return card - 5             'special attacks
                End If
            Case 3                       'Guillo
                If card < 7 Then
                    Return card + 43            'regular attacks
                Else
                    Return card + 21            'special attacks
                End If
            Case 2                       'Milly
                Dim EX As Integer = Me.EX(index)
                Select Case card
                    Case < 10
                        If (EX = 41 Or EX = 42) And card = 2 Then            'Medium Attack * (Reverse Knight, Reverse Tail)
                            Return 26
                        End If
                        If EX = 43 And card = 5 Then                         'Medium Attack B * (Trail Rush)
                            Return 30
                        End If
                        If EX = 44 And card = 5 Then                         'Medium Attack B ** (Horse Prance)
                            Return 31
                        End If
                        If EX = 45 And card = 3 Then                         'Strong Attack * (Capricorn Header)
                            Return 28
                        End If
                        If (EX > 45 And EX < 49) And card = 2 Then           'Medium Attack ** (Moon Crash, Paralysis Bell, Empyreal Thunder)
                            Return 27
                        End If
                        If (EX > 45 And EX < 49) And card = 3 Then           'Strong Attack ** (Moon Crash, Paralysis Bell, Empyreal Thunder)
                            Return 29
                        End If
                        If (EX > 48 And EX < 55) And card = 5 Then           'Medium Attack B ** (Dancing Doll, Dancing Drop, Dancing Condor, Arabesque Dance, Arabesque Doll, Arabesque Thunder)
                            Return 31
                        End If
                        Return card + 15            'regular attacks
                    Case 10
                        If EX > 48 And EX < 55 Then                          'Rabbit Dash * (Dancing Doll, Dancing Drop, Dancing Condor, Arabesque Dance, Arabesque Doll, Arabesque Thunder)
                            Return 32
                        End If
                        If EX = 55 Or EX = 56 Then                           'Rabbit Dash ** (Secret Queen, Secret Queen II)
                            Return 33
                        End If
                        If index = cards - 1 And (index = 0 OrElse Not IsAttack(combo(index - 1).Tag)) Then     'the Rabbit Dash attack only happens in isolation, i.e. no preceeding or subsequent attacks
                            Return 25
                        End If
                        Return 21                   'Rabbit Dash turns into Strong Attack B if used in a regular combo
                    Case < 24
                        Return card + 13            'special attacks up to Arabesque
                    Case 24
                        Dim equip As Integer = Me.equip(index)                         'There are 3 variations of Arabesque. Which one is used depends on the element of the weapon equipped at the start of Milly's turn.
                        If magnus_element(equip) = 1 And IsWeapon(equip) Then
                            Return 38                                                  'Arabesque (fire weapon)
                        End If
                        If magnus_element(equip) = 3 And IsWeapon(equip) Then
                            Return 39                                                  'Arabesque (lightning weapon)
                        End If
                        Return 37                                                      'Arabesque (physical weapon or no weapon)
                    Case Else
                        Return card + 15            'special attacks beyond Arabesque
                End Select
        End Select
        Return -1
    End Function

    Private Function Decimals(value As Double) As String
        Dim pre_decimal_length As Integer = value.ToString.IndexOf(".")
        If pre_decimal_length = -1 Then
            pre_decimal_length = value.ToString.Length
        End If
        Dim decimal_places As Integer = Clamp(6 - pre_decimal_length, 0, 2)
        Return FormatNumber(Math.Round(Math.Round(value, 12), decimal_places, MidpointRounding.AwayFromZero), decimal_places, TriState.True, TriState.False, TriState.False)
    End Function

    Private Function LevelToOffense(level As Integer) As Double
        If level < 5 Then
            Return 1 + (level - 1) * 0.125
        Else
            Return 1 + level * 0.1
        End If
    End Function

    Private Sub SwitchCharacter(sender As Object, e As EventArgs)
        If Not enemy_mode And sender.Tag = character Then
            Return
        End If
        character = sender.Tag
        If Not enemy_mode Then
            If cards > 0 AndAlso character <> member(cards - 1) Then    'enable relay combo
                relay = True
            Else
                relay = False
            End If
        Else
            E_target.Image = char_icon(character - 1)
            GetPartyData()
        End If
        ShowDeck()
        ScrollToStart()
    End Sub

    Public Sub ChangePartyOrder()
        For x = 0 To 2
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 75
            char_image(x).Top = 10 + top
            level_selector(x).Top = 24 + top
            actual_level(x).Top = 50 + top
            aura_type(x).Top = 24 + top
            aura_level(x).Top = 50 + top
            equipment(x).Top = 16 + top
            eq_durability(x).Top = 37 + top
        Next
    End Sub

    Private Sub AddCard(sender As Object, e As MouseEventArgs)
        sender.Focus()
        If e.Button = MouseButtons.Middle Then
            Return
        End If

        Dim id As Integer = sender.Tag
        Dim member As Integer = character - 1

        'right-click to pre-equip a magnus
        If e.Button = MouseButtons.Right Or enemy_mode Then
            If spirit_number(id) = 0 Or spirit_number(id) = 1 Then
                If equipment(member).Tag <> id Then
                    AddEquipment(member, id)
                Else
                    RemoveEquipment(equipment(member), New EventArgs)
                End If
            End If
            Return
        End If

        'transparent cards can't be added to the combo
        If hand(sender.Name).Cursor = Cursors.Default Then
            Return
        End If

        If cards = 125 Then
            MsgBox("Too many cards!")
            Return
        End If

        'add card to combo
        With combo(cards)
            .Image = magnus_image(id)
            .Tag = id
            If Not card_panel(1).Contains(combo(cards)) Then
                .Left += card_panel(1).AutoScrollPosition.X
                card_panel(1).Controls.Add(combo(cards))
            End If
            .Show()
        End With
        Me.member(cards) = character

        cards += 1
        UpdateTurns()
        If MP.Visible Then
            ChangeMP(cards - 1)
        End If
        CheckCards()
        Calculate()
        ScrollToEnd()
    End Sub

    Private Sub RemoveCard(sender As Object, e As MouseEventArgs)
        card_panel(1).Focus()
        If e.Button = MouseButtons.Right Then
            RemoveSingleCard(sender, e)
            Return
        End If

        Dim cards_prev As Integer = cards
        cards = sender.Name

        If enemy_mode Then
            For x = cards To 9
                combo(x).Hide()
            Next
            E_ShowDeck()
            E_Calculate()
            Return
        End If

        'switch characters
        If character <> member(cards) Then
            character = member(cards)
            ShowDeck()
            ScrollToStart()
        End If

        'clear cards and data beyond remaining cards
        For x = cards To 124
            combo(x).Hide()
            member(x) = 0
            equip(x) = 0
            EX(x) = 0
        Next

        UpdateTurns()
        If MP.Visible Then
            If e.Clicks = -1 Then           'after clicking "Next combo"
                MP.MP.Text = current_MP
                DisplayMP(current_MP)
            Else
                ChangeMP(cards_prev - 1)
            End If
        End If
        CheckCards()
        Calculate()
    End Sub

    Private Sub RemoveSingleCard(sender As Object, e As MouseEventArgs)
        Dim card As Integer = sender.Name
        Dim id As Integer = sender.Tag

        If enemy_mode Then
            cards -= 1
            For x = card To cards - 1
                combo(x).Image = combo(x + 1).Image
                combo(x).Tag = combo(x + 1).Tag
                combo(x).AccessibleName = combo(x + 1).AccessibleName
            Next
            For x = cards To 9
                combo(x).Hide()
            Next
            E_ShowDeck()
            E_Calculate()
            Return
        End If

        'can't remove mid-combo relay weak attack
        If spirit_number(id) = 2 And card < cards - 1 And turns > 1 And card >= first_card(1) Then
            Return
        End If

        'can't remove special attack between regular attacks (or between regular attack and equipment)
        If spirit_number(id) > 6 AndAlso (card > 0 AndAlso spirit_number(combo(card - 1).Tag) < 7) And (card < cards - 1 AndAlso spirit_number(combo(card + 1).Tag) < 7) Then
            Return
        End If

        cards -= 1
        For x = card To cards - 1
            combo(x).Image = combo(x + 1).Image
            combo(x).Tag = combo(x + 1).Tag
            member(x) = member(x + 1)
        Next
        For x = cards To 124
            combo(x).Hide()
            member(x) = 0
            equip(x) = 0
            EX(x) = 0
        Next

        UpdateTurns()
        If MP.Visible Then
            DisplayMP(current_MP)
        End If
        CheckCards()
        Calculate()
    End Sub

    Private Sub UpdateTurns()
        turns = 0
        For x = 0 To cards - 1
            If x = 0 OrElse member(x) <> member(x - 1) Then
                first_card(turns) = x
                turns += 1
            End If
            MP_gain(x) = x + 1 - first_card(turns - 1)
        Next
        If cards > 0 AndAlso character <> member(cards - 1) Then                'enable relay combo
            relay = True
        Else
            relay = False
        End If
        If cards > 1 AndAlso spirit_number(combo(cards - 1).Tag) = 1 Then       'relay combo using equipment magnus
            relay_equip = True
        Else
            relay_equip = False
        End If
    End Sub

    Private Sub AddEquipment(member As Integer, eq As Integer)
        With equipment(member)
            .Tag = eq
            .Image = New Bitmap(magnus_image(eq), .Size)
            .Show()
        End With
        With eq_durability(member)
            .Items.Clear()

            Dim first, extra As Integer
            If durability(eq) > 0 Then
                first = 1
            End If
            If My.Settings.SaberDragonHorn Then
                extra = 5
            ElseIf durability(eq) = 0 Then
                first = 1
            End If

            For x = first To durability(eq) + extra
                .Items.Add(x)
            Next
            If .Items.Count > 0 Then
                .SelectedIndex = durability(eq) - first
                .Show()
            Else
                .Hide()
            End If
        End With
        GetPartyData()
        Calculate()
    End Sub

    Private Sub RemoveEquipment(sender As Object, e As EventArgs)
        Dim chr As Integer = sender.Name - 1
        equipment(chr).Tag = 0
        equipment(chr).Hide()
        eq_durability(chr).Hide()
        GetPartyData()
        Calculate()
    End Sub

    Private Sub RemoveCardFromDeck(sender As Object, e As MouseEventArgs)
        sender.Focus()
        If e.Button <> MouseButtons.Middle Then
            Return
        End If
        If Deck.Visible Then
            Deck.ToggleCard(sender, e)
        Else
            deck_magnus(sender.Tag) = "0"
            ShowDeck()
        End If
    End Sub

    Private Sub HighlightHits(sender As Object, e As EventArgs)
        If Not My.Settings.HighlightHits Then
            Return
        End If
        If enemy_mode Then
            E_HighlightHits(sender, e)
            Return
        End If
        Dim card As Integer = sender.Name
        For x = first_hit(card) To 999
            If hit_card(x) <> card Then
                Exit For
            End If
            For y = 0 To rows - 1
                If Not hit_modifier_row.Contains(y) AndAlso table(x, y).BackColor = default_color Then
                    table(x, y).BackColor = Color.LightBlue
                End If
            Next
        Next
        highlight = card
    End Sub

    Private Sub UnhighlightHits(sender As Object, e As EventArgs)
        If enemy_mode Then
            E_UnhighlightHits(sender, e)
            Return
        End If
        highlight = -1
        Dim card As Integer = sender.Name
        For x = first_hit(card) To 999
            If hit_card(x) <> card Then
                Exit For
            End If
            For y = 0 To rows - 1
                If Not hit_modifier_row.Contains(y) AndAlso table(x, y).BackColor = Color.LightBlue Then
                    table(x, y).BackColor = default_color
                End If
            Next
        Next
    End Sub

    Private Function IsAttack(magnus As Integer) As Boolean
        If magnus_type(magnus) = 2 Or magnus_type(magnus) = 3 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsWeapon(magnus As Integer) As Boolean
        If magnus_type(magnus) = 4 Or magnus_type(magnus) = 12 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsArmor(magnus As Integer) As Boolean
        If magnus_type(magnus) = 5 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsAccessory(magnus As Integer) As Boolean
        If magnus_type(magnus) = 6 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function IsItem(magnus As Integer) As Boolean
        If magnus_type(magnus) = 7 Or magnus_type(magnus) = 8 Or magnus_type(magnus) = 9 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub CheckCombo(turn As Integer)
        Dim combo_string As String = ""
        Dim first_card, last_card, total_length, final_combo, combo_start, combo_length As Integer
        Dim second_pass As Boolean
        first_card = Me.first_card(turn)
        If Not IsAttack(combo(first_card).Tag) Then
            first_card += 1
        End If
        If turns > turn + 1 Then
            last_card = Me.first_card(turn + 1) - 1
        Else
            last_card = cards - 1
        End If

        'create a text string out of all the cards making up the turn, excluding equipment
        For x = first_card To last_card
            If turn + 1 > turns And x = Me.first_card(turn + 1) Then
                Exit For
            End If
            combo_string &= Convert.ToChar(combo(x).Tag + 34)
        Next
        If combo_string = "" Then
            Return
        End If
        total_length = combo_string.Length

        'select range of EX combos to check based on the character
        Dim first, last As Integer
        Select Case member(first_card)
            Case 1          'Sagi
                first = 1
                last = 40
            Case 2          'Milly
                first = 41
                last = 68
            Case 3          'Guillo
                first = 69
                last = 101
        End Select

        'compare string with predefined EX combo strings and find the longest EX combo
        Dim temp As Integer() = FindEXCombo(first, last, combo_string, first_card)
        final_combo = temp(0)
        combo_start = temp(1)
        combo_length = temp(2)
        Dim final_card As Integer = first_card + combo_start + combo_length - 1

        SaveEXCombo(turn, first_card, last_card, final_combo, combo_start, combo_length)

        'check if there is room left for another EX combo in the same turn; trim string
        If final_combo > 0 Then
            'Sagi and Guillo
            If member(first_card) = 1 Or member(first_card) = 3 Then
                'if EX combo ends with spirit number 4, check 5 and 6
                If spirit_number(combo(final_card).Tag) = 7 And total_length >= 6 Then
                    combo_string = combo_string.Remove(0, 4)
                    second_pass = True
                    first_card += 4
                End If
            Else    'Milly
                'if EX combo ends with spirit number <= 3, check 4 through 6
                If spirit_number(combo(final_card).Tag) <= 6 And total_length >= combo_length + 2 Then
                    If combo_start > 0 And spirit_number(combo(first_card).Tag) = 2 Then
                        combo_string = combo_string.Remove(0, 1 + combo_length)                 'ignore weak attack before Trail Rush or Capricorn Header
                    Else
                        combo_string = combo_string.Remove(0, combo_length)
                    End If
                    second_pass = True
                    first_card = final_card + 1
                    'if EX combo starts with spirit number 4, check 1 through 3
                ElseIf spirit_number(combo(first_card + combo_start).Tag) = 7 And total_length > combo_length Then
                    combo_string = combo_string.Remove(total_length - combo_length, combo_length)
                    second_pass = True
                End If
            End If
        End If

        'find second EX combo
        If second_pass Then
            last_card = first_card + combo_string.Length - 1

            temp = FindEXCombo(first, last, combo_string, first_card)
            final_combo = temp(0)
            combo_start = temp(1)
            combo_length = temp(2)

            If final_combo > 0 Then
                SaveEXCombo(turn, first_card, last_card, final_combo, combo_start, combo_length)
            End If
            Return
        End If

        'Guillo's retroactive EX combo bonus: if any of Guillo's standard attacks (except Medium Attack) directly precedes an EX combo,
        'the projectile will likely hit the target after the EX combo bonus becomes active (depends on the attack and the distance between Guillo and the enemy)
        If My.Settings.GuilloExtraBonus AndAlso (member(first_card) = 3 And final_combo > 0 And combo_start > 0) Then
            Dim attacks() As Integer = {1, 3, 4, 5, 6}
            Dim card_before_EX As Integer = first_card + combo_start - 1
            If attacks.Contains(combo(card_before_EX).Tag) Then
                EX(card_before_EX) = final_combo
            End If
        End If
    End Sub

    Private Function FindEXCombo(first As Integer, last As Integer, combo_string As String, first_card As Integer) As Integer()
        Dim final_combo, combo_start, final_length, requirement, equip, length As Integer
        For x = first To last
            If combo_string.Contains(EX_string(x)) Then
                length = EX_combo_data(x, 0)
                If length <= final_length Then
                    If final_combo <> 52 Or x > 54 Then                 'if final combo is Arabesque Dance, check for Arabesque Doll and Arabesque Thunder
                        Continue For
                    End If
                End If
                requirement = EX_combo_data(x, 10)
                equip = Me.equip(first_card)

                If requirement >= 1 And requirement <= 4 Then           'EX combos requiring specific weapon elements
                    If magnus_element(equip) <> requirement Or Not IsWeapon(equip) Then
                        Continue For
                    End If
                End If

                If requirement = 5 And Not IsArmor(equip) Then          'EX combos requiring armor
                    Continue For
                End If

                If requirement = 6 And Not IsAccessory(equip) Then      'EX combos requiring an accessory
                    Continue For
                End If

                If requirement = 7 AndAlso Not down.Checked Then        'enemy down (Secret Queen combos)
                    Continue For
                End If

                final_combo = x
                combo_start = combo_string.IndexOf(EX_string(x))
                final_length = length

                If (final_combo = 55 Or final_combo = 56) And combo_start > 0 Then      'Secret Queen: only if combo starts with Rabbit Dash
                    final_combo = 0
                    combo_start = 0
                    final_length = 0
                End If
            End If
        Next
        Return {final_combo, combo_start, final_length}
    End Function

    Private Sub SaveEXCombo(turn As Integer, first_card As Integer, last_card As Integer, final_combo As Integer, combo_start As Integer, combo_length As Integer)
        For x = first_card To last_card
            If turn + 1 > turns And x = Me.first_card(turn + 1) Then
                Exit For
            End If
            If x < first_card + combo_start Then
                EX(x) = 0
            ElseIf x < first_card + combo_start + combo_length Then
                EX(x) = final_combo
            Else
                EX(x) = 0
            End If
        Next
    End Sub

    Public Sub ShowDeck()
        Dim y As Integer = 0

        'show equipment magnus
        For x = 45 To 452
            If (magnus_user(x) = 0 Or magnus_user(x) = character) And deck_magnus(x) = "1" And Not IsItem(x) Then
                hand(y).Image = magnus_image(x)
                hand(y).Tag = x
                If Not card_panel(0).Contains(hand(y)) Then
                    hand(y).Left += card_panel(0).AutoScrollPosition.X
                    card_panel(0).Controls.Add(hand(y))
                End If
                hand(y).Show()
                y += 1
            End If
        Next

        If enemy_mode Then
            For x = y To 299
                hand(x).Hide()
            Next
            Return
        End If

        'show attack magnus
        For x = 1 To 44
            If (magnus_user(x) = 0 Or magnus_user(x) = character) And deck_magnus(x) = "1" Then
                hand(y).Image = magnus_image(x)
                hand(y).Tag = x
                If Not card_panel(0).Contains(hand(y)) Then
                    hand(y).Left += card_panel(0).AutoScrollPosition.X
                    card_panel(0).Controls.Add(hand(y))
                End If
                hand(y).Show()
                y += 1
            End If
        Next

        For x = y To 299
            hand(x).Hide()
        Next
        CheckCards()
    End Sub

    Public Sub CheckCards()
        Dim transparent As Boolean
        For x = 0 To 299
            If Not hand(x).Visible Then
                Exit For
            End If
            transparent = False
            Dim id As Integer = hand(x).Tag

            'spirit number too low
            If Not relay AndAlso cards > 0 AndAlso spirit_number(id) <= spirit_number(combo(cards - 1).Tag) Then
                transparent = True
            End If

            'equipping a card with no spirit number
            If cards = 1 AndAlso spirit_number(combo(0).Tag) = 0 Then
                transparent = True
            End If

            'after switching characters: not a weak attack, not an equipment magnus with relay mark
            If relay AndAlso (spirit_number(id) < 1 Or spirit_number(id) > 2 OrElse (spirit_number(id) = 1 And Not relay_mark(id))) Then
                transparent = True
            End If

            'after relay-equipping a magnus: not a weak attack
            If relay_equip And spirit_number(id) <> 2 Then
                transparent = True
            End If

            'after switching characters: last card was not a special attack
            If cards > 0 AndAlso spirit_number(combo(cards - 1).Tag) < 7 AndAlso member(cards - 1) <> character Then
                transparent = True
            End If

            'only for X relays (infinite combo): same character can only attack again after two more turns
            If turns > 1 AndAlso character = member(first_card(turns - 2)) Then
                transparent = True
            End If

            'not enough MP
            If MP.Visible AndAlso MP_cost(id) * 100 > current_MP Then
                transparent = True
            End If

            If transparent Then
                hand(x).Image = MakeTransparent(magnus_image(id))
                hand(x).Cursor = Cursors.Default
            Else
                hand(x).Image = magnus_image(id)
                hand(x).Cursor = Cursors.Hand
            End If
        Next
    End Sub

    Public Sub CheckQuestMagnus()
        Dim effect, bonus As Integer
        Dim physical, fire, ice, lightning, light, dark, extra, crit, level, defense, knockdown, knockout, sagi_HP, milly_HP, guillo_HP As Integer
        For x = 0 To 23
            effect = QM_effect(QM_inventory(x))
            bonus = QM_bonus(QM_inventory(x))
            Select Case effect
                Case 9
                    fire += bonus
                Case 10
                    ice += bonus
                Case 11
                    lightning += bonus
                Case 12
                    light += bonus
                Case 13
                    dark += bonus
                Case 14
                    physical += bonus
                Case 15
                    physical += bonus
                    fire += bonus
                    ice += bonus
                    lightning += bonus
                    light += bonus
                    dark += bonus
                    extra += bonus
                Case 22
                    level += bonus
                Case 27
                    defense += bonus
                Case 32
                    sagi_HP += bonus
                Case 33
                    milly_HP += bonus
                Case 34
                    guillo_HP += bonus
                Case 35
                    sagi_HP += bonus
                    milly_HP += bonus
                    guillo_HP += bonus
                Case 36
                    fire += bonus
                Case 37
                    ice += bonus
                Case 38
                    lightning += bonus
                Case 41
                    light += bonus
                    dark -= bonus
                Case 42
                    light -= bonus
                    dark += bonus
                Case 43
                    physical += bonus
                    fire += bonus
                Case 46
                    knockdown += bonus
                Case 47
                    knockout += bonus
                Case 48
                    crit += bonus
            End Select
        Next

        QM_total_bonus(0) = physical
        QM_total_bonus(1) = fire
        QM_total_bonus(2) = ice
        QM_total_bonus(3) = lightning
        QM_total_bonus(4) = light
        QM_total_bonus(5) = dark
        QM_total_bonus(6) = extra
        QM_total_bonus(7) = Math.Min(crit, 100)
        QM_total_bonus(8) = level
        QM_total_bonus(9) = defense
        QM_total_bonus(10) = knockdown
        QM_total_bonus(11) = knockout
        QM_total_bonus(12) = sagi_HP
        QM_total_bonus(13) = milly_HP
        QM_total_bonus(14) = guillo_HP

        If QuestMagnus.Visible Then
            For x = 0 To 8
                If x = 7 Then
                    QuestMagnus.result(x + 9).Text = QM_total_bonus(x) & "%"
                Else
                    QuestMagnus.result(x + 9).Text = AddSign(QM_total_bonus(x))
                End If
            Next
            For x = 9 To 11
                QuestMagnus.E_result(x - 3).Text = AddSign(QM_total_bonus(x))
            Next
            For x = 12 To 14
                QuestMagnus.E_result(x - 3).Text = AddSign(QM_total_bonus(x)) & "%"
            Next
        End If

        'change level (Hero License, Tub-Time Greythorne)
        For x = 0 To 2
            CheckLevel(level_selector(x), New EventArgs)
        Next

        GetPartyData()
        Calculate()
    End Sub

    Private Function AddSign(number As Integer) As String
        If number > 0 Then
            Return "+" & number
        Else
            Return number
        End If
    End Function

    Private Sub CheckAura(chr As Integer, go As Boolean)
        'aura properties based on type and aura level
        Dim type, lv, index, start_level, end_level, hp, crush_limit, down, element, attack, crush, defense(6) As Integer
        type = aura(chr, 0)
        lv = aura(chr, 1)
        index = type * 3 + lv

        'select character's level range based on aura level
        Select Case lv
            Case 0
                start_level = 1
                end_level = 15
            Case 1
                start_level = 15
                end_level = 30
            Case 2
                start_level = 30
                end_level = 50
        End Select

        hp = aura_data(index, 0)
        crush_limit = aura_data(index, 1)
        down = aura_data(index, 2)
        element = aura_data(index, 3)
        attack = aura_data(index, 4)
        crush = aura_data(index, 5)
        For x = 0 To 5
            defense(x) = aura_data(index, x + 6)
        Next

        Dim factor As Double = (level(chr) - start_level) / (end_level - start_level)
        factor = Clamp(factor, 0, 1)

        hp = Math.Round(hp * (0.8 + 0.2 * factor), 0, MidpointRounding.AwayFromZero)
        crush_limit = Math.Round(crush_limit * (0.8 + 0.2 * factor), 0, MidpointRounding.AwayFromZero)
        down = Math.Round(down * (0.8 + 0.2 * factor), 0, MidpointRounding.AwayFromZero)
        attack = Math.Round(attack * (0.8 + 0.2 * factor), 0, MidpointRounding.AwayFromZero)
        crush = Math.Round(crush * (0.8 + 0.2 * factor), 0, MidpointRounding.AwayFromZero)
        For x = 0 To 6
            defense(x) = Math.Round(defense(x) * (0.8 + 0.2 * factor), 0, MidpointRounding.AwayFromZero)
            If defense(x) < 0 Then
                defense(x) += 1
            End If
        Next

        aura_HP(chr) = hp
        aura_crush_limit(chr) = crush_limit
        aura_down(chr) = down

        'put each element's attack/crush/defense bonus into an array
        For x = 0 To 5
            If x = element Or element = 6 Then
                aura_offense(chr, x) = attack
                aura_crush(chr, x) = crush
            Else
                aura_offense(chr, x) = 0
                aura_crush(chr, x) = 0
            End If
            aura_defense(chr, x) = defense(x)
        Next

        GetPartyData()
        If go Then
            Calculate()
        End If
    End Sub

    Public Sub ChangeTarget(new_target As Integer, new_HP As Integer, go As Boolean)
        Dim old_target As Integer = combo_target
        combo_target = new_target
        target_image.Image = New Bitmap(My.Resources.ResourceManager.GetObject("target_" & new_target), New Size(110, 132))
        If Boost.Visible Then
            Boost.character(3).Image = New Bitmap(My.Resources.ResourceManager.GetObject("target_" & new_target), New Size(70, 84))
        End If

        target_data(0).Text = enemy_name(new_target)
        If knockdown(new_target) > 0 Then
            target_data(1).Text = "Knockdown"
            target_data(2).Text = knockdown(new_target)
        Else
            target_data(1).Text = ""
            target_data(2).Text = ""
        End If
        If knockout(new_target) > 0 Then
            target_data(3).Text = "Knockout"
            target_data(4).Text = knockout(new_target)
        Else
            target_data(3).Text = ""
            target_data(4).Text = ""
        End If

        If Not enemy_mode Then
            If multi_phase_enemies.Contains(new_target) Then
                target_data(11).Hide()
                final_phase.Show()
            Else
                final_phase.Hide()
                target_data(11).Show()
            End If

            If shield_limit(new_target) > 0 And Not (final_phase.Visible And Not final_phase.Checked) Then
                target_data(12).Hide()
                shield.Show()
            Else
                shield.Hide()
                target_data(12).Show()
            End If
        Else
            With target_data(0)
                Dim text_width As Integer = TextRenderer.MeasureText(.Text, .Font).Width
                If text_width <= .Width Then
                    .Size = New Size(125, 24)
                Else
                    .Size = New Size(125, 39)
                End If
            End With
            E_ShowDeck()
        End If

        true_max_HP = HP(new_target)
        target_data(8).Text = true_max_HP
        If new_HP < 0 Or new_HP > true_max_HP Then
            ChangeHP(true_max_HP, True)
        Else
            ChangeHP(new_HP, True)
        End If
        enemy_HP.ForeColor = Color.Black

        If new_target <> old_target Then
            ChangeArmor()
            If enemy_mode And cards > 0 Then
                RemoveCard(combo(0), New MouseEventArgs(0, -1, 0, 0, 0))
            End If
        ElseIf Not enemy_mode Then
            Select Case new_target
                Case 26, 65, 85, 87
                    target_data(10).Show()
                    armor_durability.Show()
            End Select
        End If
        If enemy_mode And new_target = 117 Then
            E_armor.Show()
        Else
            E_armor.Hide()
        End If
        If enemy_mode And new_target = 70 Then
            E_target_data(6).Show()
            E_prima_queens.Show()
        Else
            E_target_data(6).Hide()
            E_prima_queens.Hide()
        End If
        If go Then
            Calculate()
        End If
    End Sub

    Private Sub ChangeArmor()
        Dim armor_durability As Integer
        Select Case combo_target
            Case 26                     'Armored Cancerite
                armor_defense = {50, 50, 50, 50, 50, 50}
                armor_durability = 8
            Case 65                     'Armored Balloona
                armor_defense = {200, 200, 200, 200, 200, 200}
                armor_durability = 3
            Case 85                     'Armored Mite
                armor_defense = {150, 100, 100, 150, 100, 100}
                armor_durability = 3
            Case 87                     'Phoelix
                armor_defense = {50, 50, 50, 50, 50, 50}
                armor_durability = 5
            Case 117                    'Machina Arma: Razer 3
                armor_defense = {20, 20, 20, 20, 20, 20}
            Case Else
                armor_defense = {0, 0, 0, 0, 0, 0}
        End Select

        If armor_durability = 0 Then
            target_data(10).Hide()
            Me.armor_durability.Hide()
            Me.armor_durability.Items.Clear()
            Return
        End If

        With Me.armor_durability
            .Items.Clear()
            For i = 0 To armor_durability
                .Items.Add(i)
            Next
            .SelectedIndex = .Items.Count - 1
            If Not enemy_mode Then
                target_data(10).Show()
                .Show()
            End If
        End With
    End Sub

    Private Sub CheckHP()
        Dim new_HP As String = enemy_HP.Text
        If new_HP = "" Then
            enemy_HP.ForeColor = Color.Red
            ChangeHP(0, False)
            Calculate()
            Return
        End If
        If Not IsNonNegativeInteger(new_HP) Then
            ResetHP()
            Return
        End If
        If new_HP > true_max_HP Then
            enemy_HP.ForeColor = Color.Red
            ChangeHP(true_max_HP, False)
        Else
            enemy_HP.ForeColor = Color.Black
            ChangeHP(new_HP, False)
        End If
        Calculate()
    End Sub

    Private Sub E_CheckHP()
        Dim new_HP As String = E_HP_box.Text
        If new_HP = "" Then
            E_HP_box.ForeColor = Color.Red
            E_ChangeHP(0)
            Calculate()
            Return
        End If
        If Not IsNonNegativeInteger(new_HP) Then
            E_ResetHP()
            Return
        End If
        If new_HP > member_max_hp_armor Then
            E_HP_box.ForeColor = Color.Red
            new_HP = member_max_hp_armor
        Else
            E_HP_box.ForeColor = Color.Black
        End If
        E_ChangeHP(new_HP)
        E_Calculate()
    End Sub

    Private Sub E_ChangeHP(new_HP As Integer)
        member_hp = new_HP
        If member_hp <> member_max_hp_armor Then
            E_target_data(1).BackColor = Color.LightYellow
            E_target_data(1).Cursor = Cursors.Hand
        Else
            E_target_data(1).BackColor = default_color
            E_target_data(1).Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ChangeHP(new_HP As Integer, change_text As Boolean)
        true_HP = new_HP
        If change_text Then
            enemy_HP.Text = new_HP
        End If
        If Not (final_phase.Visible And final_phase.Checked) And HP_limit(combo_target) > 0 Then
            effective_max_HP = Math.Floor(true_max_HP * Round(1 - HP_limit(combo_target))) + 1
            effective_HP = Math.Max(0, true_HP + effective_max_HP - true_max_HP)
            target_data(7).Text = effective_HP
            target_data(9).Text = effective_max_HP
            If Not enemy_mode Then
                target_data(6).Show()
                target_data(7).Show()
                target_data(9).Show()
            End If
        Else
            effective_max_HP = true_max_HP
            effective_HP = true_HP
            target_data(6).Hide()
            target_data(7).Hide()
            target_data(9).Hide()
        End If
        If true_HP <> true_max_HP Then
            target_data(8).BackColor = Color.LightYellow
            target_data(8).Cursor = Cursors.Hand
        Else
            target_data(8).BackColor = default_color
            target_data(8).Cursor = Cursors.Default
        End If
        If shield.Visible Then
            If true_HP < Math.Floor(true_max_HP * shield_limit(combo_target)) Then
                shield.Enabled = False
                shield.Checked = False
            Else
                shield.Enabled = True
                shield.Checked = True
            End If
        End If
    End Sub

    Private Sub ResetHP()
        enemy_HP.Text = HP(combo_target)
    End Sub

    Private Sub E_ResetHP()
        E_HP_box.Text = member_max_hp_armor
    End Sub

    Private Sub ScrollHP(sender As Object, e As MouseEventArgs)
        FixHP(sender, e)
        Dim max As Integer
        If Not enemy_mode Then
            max = true_max_HP
        Else
            max = member_max_hp_armor
        End If
        If e.Delta > 0 Then
            If sender.Text < max Then
                sender.Text += 1
            End If
        Else
            If sender.Text > 0 Then
                sender.Text -= 1
            End If
        End If
    End Sub

    Private Sub ToggleFinalPhase()
        'the following bosses have a shield that can only break during the final phase
        'Machina Arma: Razer 3, Lord of the Lava Caves, Promachina Shanath, and the true final boss
        If combo_target = 117 Or combo_target = 131 Or combo_target = 133 Or combo_target = 140 Then
            If final_phase.Checked Then
                target_data(12).Hide()
                shield.Show()
                If Not shield.Checked Then
                    If knockdown(combo_target) > 0 Then
                        target_data(2).Text = Math.Floor(knockdown(combo_target) * 0.8)
                    End If
                    If knockout(combo_target) > 0 Then
                        target_data(4).Text = Math.Floor(knockout(combo_target) * 0.8)
                    End If
                End If
            Else
                shield.Hide()
                target_data(12).Show()
                If knockdown(combo_target) > 0 Then
                    target_data(2).Text = knockdown(combo_target)
                End If
                If knockout(combo_target) > 0 Then
                    target_data(4).Text = knockout(combo_target)
                End If
            End If
        End If
        ChangeHP(true_HP, False)
        Calculate()
    End Sub

    Private Sub ToggleShield()
        If knockdown(combo_target) > 0 Then
            If shield.Checked Then
                target_data(2).Text = knockdown(combo_target)
            Else
                target_data(2).Text = Math.Floor(knockdown(combo_target) * 0.8)
            End If
        End If
        If knockout(combo_target) > 0 Then
            If shield.Checked Then
                target_data(4).Text = knockout(combo_target)
            Else
                target_data(4).Text = Math.Floor(knockout(combo_target) * 0.8)
            End If
        End If
        Calculate()
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Dim textbox As Boolean
        If TypeOf ActiveControl Is TextBox Then
            textbox = True
        End If
        For x = 0 To 2
            If ActiveControl Is level_selector(x) Then
                textbox = True
            End If
        Next
        If Not textbox Then
            Dim number As Integer
            Select Case e.KeyCode
                Case Keys.D1, Keys.NumPad1
                    number = 1
                Case Keys.D2, Keys.NumPad2
                    number = 2
                Case Keys.D3, Keys.NumPad3
                    number = 3
                Case Keys.Back
                    If cards > 0 Then
                        RemoveCard(combo(cards - 1), New MouseEventArgs(0, 0, 0, 0, 0))
                    End If
                    Return
            End Select
            If number > 0 Then
                Dim character As Integer = My.Settings.PartyOrder.Substring(number - 1, 1)
                SwitchCharacter(char_image(character), e)
                Return
            End If
        End If

        Select Case e.KeyCode
            Case Keys.E
                Dolphin()
            Case Keys.C
                CopyTable()
            Case Keys.N
                If cards > 0 Then
                    NextCombo()
                End If
            Case Keys.R
                ChangeSize()
            Case Else
                SelectWindow(sender, e)
        End Select
    End Sub

    Public Function MakeTransparent(img As Image) As Bitmap
        Dim bmp As New Bitmap(img.Width, img.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Dim colormatrix As New ColorMatrix
        colormatrix.Matrix33 = 0.5
        Dim imgAttribute As New ImageAttributes
        imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.[Default], ColorAdjustType.Bitmap)
        g.DrawImage(img, New Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute)
        g.Dispose()
        Return bmp
    End Function

    Private Sub ResizePanel()
        card_panel(0).Width = Width - 16 - card_panel(0).Left
        card_panel(1).Width = Width - 16
        output_panel.Size = New Size(Width - 16, Height - 490)
        E_output_panel.Size = New Size(Width - 16, Height - 490)
    End Sub

    Private Sub SaveSettings() Handles Me.FormClosing
        With My.Settings
            .MagnusActive.Clear()
            .MagnusActive.AddRange(deck_magnus)
            .Target = combo_target
            .FinalPhase = final_phase.Checked
            .Character = character - 1
            For x = 0 To 2
                FixLevel(level_selector(x), New EventArgs)
            Next
            .SagiLevel = level_selector(0).Text
            .MillyLevel = level_selector(1).Text
            .GuilloLevel = level_selector(2).Text

            Dim auras As String = ""
            For x = 0 To 2
                auras &= Hex(aura_type(x).SelectedIndex)
                auras &= aura_level(x).SelectedIndex
            Next
            .Auras = auras

            Dim quest_magnus As String = ""
            For x = 0 To 23
                If QM_inventory(x) < 16 Then
                    quest_magnus &= "0" & Hex(QM_inventory(x))
                Else
                    quest_magnus &= Hex(QM_inventory(x))
                End If
            Next
            .QuestMagnus = quest_magnus
        End With
    End Sub

    Private Sub FilterInput(sender As Object, e As KeyPressEventArgs)
        If IsNumeric(e.KeyChar) Then                                                    'allow numbers
            Return
        End If
        Select Case e.KeyChar
            Case ChrW(Keys.Back), ChrW(1), ChrW(3), ChrW(22), ChrW(24), ChrW(26)        'allow backspace and Ctrl+A/C/V/X/Z
                Return
        End Select
        e.Handled = True                                                                'block everything else
    End Sub

    Private Sub FixHP(sender As Object, e As EventArgs)                                         'called when text box loses focus
        Dim hp As Integer
        If Not enemy_mode Then
            hp = true_HP
        Else
            hp = member_hp
        End If
        If sender.ForeColor = Color.Red Then
            sender.Text = hp
            Return
        End If
        'remove leading zeros
        Dim value As Integer = sender.Text
        sender.Text = value
    End Sub

    Private Sub FixLevel(sender As Object, e As EventArgs)      'called when text box loses focus
        Dim chr As Integer = sender.Tag
        If sender.ForeColor = Color.Red Then
            Dim level As Integer = Me.level(chr) - QM_total_bonus(8)
            level = Clamp(level, 1, 100)
            level_selector(chr).SelectedIndex = level - 1
            Return
        End If
        'remove leading zeros
        Dim value As Integer = sender.Text
        sender.Text = value
    End Sub

    Private Sub CheckLevel(sender As Object, e As EventArgs)
        Dim chr As Integer = sender.Tag
        Dim box As ComboBox = level_selector(chr)
        Dim new_level As String = box.Text

        If new_level = "" Then
            box.ForeColor = Color.Red
            ChangeLevel(chr, 1)
            Calculate()
            Return
        End If
        If Not IsNonNegativeInteger(new_level) Then
            box.SelectedIndex = 0
            Return
        End If
        If new_level < 1 Or new_level > 100 Then
            box.ForeColor = Color.Red
        Else
            box.ForeColor = Color.Black
        End If

        ChangeLevel(chr, new_level)
        Calculate()
    End Sub

    Private Sub ChangeLevel(chr As Integer, level As Integer)
        level = Clamp(level, 1, 100)
        Dim final_level As Integer = level + QM_total_bonus(8)
        final_level = Clamp(final_level, 1, 100)
        Me.level(chr) = final_level

        With actual_level(chr)
            If final_level > level Then
                .Text = final_level
                .ForeColor = Color.Green
                .Show()
            ElseIf final_level < level Then
                .Text = final_level
                .ForeColor = Color.Red
                .Show()
            Else
                .Text = ""
                .Hide()
            End If
        End With

        CheckAura(chr, False)
    End Sub

    Public Function IsNonNegativeInteger(text As String) As Boolean
        Dim number As Integer
        If Integer.TryParse(text, number) Then
            If number >= 0 Then
                Return True
            End If
        End If
        Return False
    End Function

    Public Function IsHexadecimal(text As String) As Boolean
        Dim chars() As Char = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"}
        For x = 0 To text.Length - 1
            Dim c As Char = text.ElementAt(x)
            If Not chars.Contains(c) Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function IsBinary(text As String) As Boolean
        Dim chars() As Char = {"0", "1"}
        For x = 0 To text.Length - 1
            Dim c As Char = text.ElementAt(x)
            If Not chars.Contains(c) Then
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub ToggleDown(sender As Object, e As EventArgs)
        If sender.Checked Then
            sender.BackColor = Color.LightYellow
        Else
            sender.BackColor = default_color
        End If
        Calculate()
    End Sub

    Private Sub ToggleEffect(sender As Object, e As EventArgs)
        Dim hit As Integer = sender.Tag
        Select Case sender.Name
            Case 7
                weapon_effect(hit) = Not weapon_effect(hit)
            Case 8
                weapon_boost(hit) = Not weapon_boost(hit)
            Case 10
                weapon_crit(hit) = Not weapon_crit(hit)
            Case 16
                qm_crit(hit) = Not qm_crit(hit)
            Case 27
                min_one(hit) = Not min_one(hit)
            Case Else
                Return
        End Select
        Calculate()
    End Sub

    Private Sub Clear(hits_prev As Integer)
        For x = hits + 1 To hits_prev
            For y = 0 To rows - 1
                If Not hit_modifier_row.Contains(y) Then
                    table(x, y).Hide()
                End If
            Next
            For y = 0 To 2
                hit_modifier(x, y).Hide()
                hit_modifier(x, y).SelectedIndex = Math.Floor((hit_modifier(x, y).Items.Count - 1) * 0.5)
            Next
            weapon_effect(x) = False
            weapon_boost(x) = False
            qm_crit(x) = False
            weapon_crit(x) = False
            knockdown_hit(x) = False
            min_one(x) = False
            hit_card(x) = -1
        Next

        Dim highlight(8) As Boolean
        For x = 1 To hits
            If hit_modifier(x, 0).SelectedIndex <> Math.Floor((hit_modifier(x, 0).Items.Count - 1) * 0.5) Then
                highlight(0) = True
                Exit For
            End If
        Next
        For x = 1 To hits
            If hit_modifier(x, 1).SelectedIndex <> 3 Then
                highlight(1) = True
                Exit For
            End If
        Next
        If Array.IndexOf(weapon_effect, True, 1, hits) >= 0 Then
            highlight(2) = True
        End If
        If Array.IndexOf(weapon_boost, True, 1, hits) >= 0 Then
            highlight(3) = True
        End If
        If Array.IndexOf(weapon_crit, True, 1, hits) >= 0 Then
            highlight(4) = True
        End If
        If Array.IndexOf(qm_crit, True, 1, hits) >= 0 Then
            highlight(5) = True
        End If
        For x = 1 To hits
            If hit_modifier(x, 2).SelectedIndex <> Math.Floor((hit_modifier(x, 2).Items.Count - 1) * 0.5) Then
                highlight(6) = True
                Exit For
            End If
        Next
        If Array.IndexOf(min_one, True, 1, hits) >= 0 Then
            highlight(7) = True
        End If
        For x = 0 To 7
            If highlight(x) Then
                table(0, clickable_rows(x)).BackColor = Color.LightYellow
            Else
                table(0, clickable_rows(x)).BackColor = default_color
            End If
        Next
    End Sub

    Private Sub ChangeSize()
        WindowState = FormWindowState.Normal
        Dim rows As Integer
        Dim panel As CustomPanel
        If Not enemy_mode Then
            rows = My.Settings.ResultsRow.Count(Function(c As Char) c = "1")
            panel = output_panel
        Else
            rows = My.Settings.EnemyResultsRow.Count(Function(c As Char) c = "1")
            panel = E_output_panel
        End If
        Width = 176 + hits * 52
        Height = 494 + rows * 25
        If panel.VerticalScroll.Visible Then
            Width += 17
        End If
        If panel.HorizontalScroll.Visible Then
            Height += 17
        End If
    End Sub

    Private Sub ChangeDeviation(sender As Object, e As EventArgs)
        Calculate()
    End Sub

    Private Sub ResetRow(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Middle Then
            Return
        End If
        Dim row As Integer = sender.Tag
        Select Case row
            Case 4                  'reset offense deviation
                For x = 1 To hits
                    hit_modifier(x, 0).SelectedIndex = Math.Floor((hit_modifier(x, 0).Items.Count - 1) * 0.5)
                Next
            Case 5                  'reset crush deviation
                For x = 1 To hits
                    hit_modifier(x, 1).SelectedIndex = 3
                Next
            Case 7                  'remove all weapon effects
                For x = 1 To hits
                    weapon_effect(x) = False
                Next
            Case 8                  'remove all weapon boosts
                For x = 1 To hits
                    weapon_boost(x) = False
                Next
            Case 10                 'remove all random Cutthroat Knife crits
                For x = 1 To hits
                    weapon_crit(x) = False
                Next
            Case 16                 'remove all random quest magnus crits
                For x = 1 To hits
                    qm_crit(x) = False
                Next
            Case 21                 'reset defense deviation
                For x = 1 To hits
                    hit_modifier(x, 2).SelectedIndex = Math.Floor((hit_modifier(x, 2).Items.Count - 1) * 0.5)
                Next
            Case 27                 'remove all "minimum 1" hits
                For x = 1 To hits
                    min_one(x) = False
                Next
            Case 31
                ToggleEffectiveHP()
                Return
            Case Else
                Return
        End Select
        table(0, row).BackColor = default_color
        Calculate()
    End Sub

    Private Sub HideRow(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Middle Then
            Return
        End If
        Dim i As Integer = sender.Tag
        Dim temp As String = My.Settings.ResultsRow
        temp = temp.Remove(i, 1)
        temp = temp.Insert(i, "0")
        My.Settings.ResultsRow = temp
        UpdateRows()
        If Settings.Visible Then
            RemoveHandler Settings.row(i).CheckedChanged, AddressOf Settings.ToggleRow
            Settings.row(i).Checked = False
            Settings.row(i).BackColor = default_color
            AddHandler Settings.row(i).CheckedChanged, AddressOf Settings.ToggleRow
        End If
    End Sub

    Private Sub NextCombo()
        If enemy_mode Then
            E_NextCombo()
            Return
        End If
        If burst_active Then
            MP.burst.Checked = False
            current_MP = 0
        End If
        Dim attack As Boolean
        If secondary_target.Checked Then
            For x = 0 To cards - 1
                If multi_target_attacks.Contains(combo(x).Tag) Then
                    attack = True
                    Exit For
                End If
            Next
        Else
            If cards > 1 OrElse IsAttack(combo(0).Tag) Then
                attack = True
            End If
        End If
        RemoveCard(combo(0), New MouseEventArgs(0, -1, 0, 0, 0))
        If attack Then
            enemy_HP.Text = post_combo_HP
            If armor_durability.Visible Then
                armor_durability.SelectedIndex = post_combo_armor
            End If
            If shield.Visible Then
                shield.Checked = post_combo_shield
            End If
            down.Checked = post_combo_down
            enemy_status.SelectedIndex = post_combo_status
        End If
        For x = 0 To 2
            If post_combo_equip(x, 0) <> 0 Then
                AddEquipment(x, post_combo_equip(x, 0))
                If eq_durability(x).Visible Then
                    If durability(post_combo_equip(x, 0)) = 0 Then
                        eq_durability(x).SelectedIndex = post_combo_equip(x, 1)
                    Else
                        eq_durability(x).SelectedIndex = post_combo_equip(x, 1) - 1
                    End If
                End If
            Else
                equipment(x).Tag = 0
                equipment(x).Hide()
                eq_durability(x).Hide()
            End If
        Next
        For x = 0 To 2
            If Not Boost.Visible Then
                For y = 0 To 5
                    offense_boost(x, y, 0) = post_combo_offense_boost(x, y, 0)
                    offense_boost(x, y, 1) = post_combo_offense_boost(x, y, 1)
                Next
            Else
                For y = 0 To 5
                    Boost.boost(x, y, 0).Text = post_combo_offense_boost(x, y, 0)
                    Boost.boost(x, y, 1).Text = post_combo_offense_boost(x, y, 1)
                Next
            End If
        Next
        If attack Then
            If Not Boost.Visible Then
                For x = 0 To 5
                    enemy_defense_boost(x, 0) = post_combo_enemy_defense_boost(x, 0)
                    enemy_defense_boost(x, 1) = post_combo_enemy_defense_boost(x, 1)
                    enemy_offense_boost(x, 0) = post_combo_enemy_offense_boost(x, 0)
                    enemy_offense_boost(x, 1) = post_combo_enemy_offense_boost(x, 1)
                Next
            Else
                For x = 0 To 5
                    Boost.boost(3, x, 0).Text = post_combo_enemy_defense_boost(x, 0)
                    Boost.boost(3, x, 1).Text = post_combo_enemy_defense_boost(x, 1)
                    Boost.boost(4, x, 0).Text = post_combo_enemy_offense_boost(x, 0)
                    Boost.boost(4, x, 1).Text = post_combo_enemy_offense_boost(x, 1)
                Next
            End If
        End If
    End Sub

    Private Sub E_NextCombo()
        RemoveCard(combo(0), New MouseEventArgs(0, 0, 0, 0, 0))
        E_HP_box.Text = post_combo_HP
        E_down.Checked = post_combo_down
        E_status.SelectedIndex = post_combo_status
        Dim x As Integer = character - 1
        If post_combo_equip(x, 0) <> 0 Then
            If eq_durability(x).Visible Then
                If durability(post_combo_equip(x, 0)) = 0 Then
                    eq_durability(x).SelectedIndex = post_combo_equip(x, 1)
                Else
                    eq_durability(x).SelectedIndex = post_combo_equip(x, 1) - 1
                End If
            End If
        Else
            equipment(x).Tag = 0
            equipment(x).Hide()
            eq_durability(x).Hide()
        End If
        For x = 0 To 2
            If Not Boost.Visible Then
                For y = 0 To 5
                    defense_boost(x, y, 0) = post_combo_defense_boost(x, y, 0)
                    defense_boost(x, y, 1) = post_combo_defense_boost(x, y, 1)
                Next
            Else
                For y = 0 To 5
                    Boost.boost(x, y, 0).Text = post_combo_defense_boost(x, y, 0)
                    Boost.boost(x, y, 1).Text = post_combo_defense_boost(x, y, 1)
                Next
            End If
        Next
        If Not Boost.Visible Then
            For x = 0 To 5
                enemy_offense_boost(x, 0) = post_combo_enemy_offense_boost(x, 0)
                enemy_offense_boost(x, 1) = post_combo_enemy_offense_boost(x, 1)
            Next
        Else
            For x = 0 To 5
                Boost.boost(4, x, 0).Text = post_combo_enemy_offense_boost(x, 0)
                Boost.boost(4, x, 1).Text = post_combo_enemy_offense_boost(x, 1)
            Next
        End If
    End Sub

    Public Sub ToggleEffectiveHP()
        My.Settings.EffectiveHPRemaining = Not My.Settings.EffectiveHPRemaining
        If My.Settings.EffectiveHPRemaining Then
            table(0, rows - 1).Text = "Effective HP remaining"
        Else
            table(0, rows - 1).Text = "HP remaining"
        End If
        If Settings.Visible Then
            Settings.row(31).Text = table(0, rows - 1).Text
            With Settings.setting(3)
                RemoveHandler .CheckedChanged, AddressOf Settings.ChangeSetting
                .Checked = My.Settings.EffectiveHPRemaining
                AddHandler .CheckedChanged, AddressOf Settings.ChangeSetting
            End With
        End If
        Calculate()
    End Sub

    Public Sub ToggleSaberDragonHorn()
        My.Settings.SaberDragonHorn = Not My.Settings.SaberDragonHorn

        Dim first, extra, before, after As Integer
        If My.Settings.SaberDragonHorn Then
            extra = 5
        End If

        For x = 0 To 2
            If Not equipment(x).Visible Then
                Continue For
            End If

            before = eq_durability(x).Items.Count
            after = durability(equipment(x).Tag) + extra
            If durability(equipment(x).Tag) = 0 Then
                first = 0
            Else
                first = 1
            End If

            With eq_durability(x)
                If after < before Then
                    For y = after To before - 1
                        .Items.RemoveAt(.Items.Count - 1)
                    Next
                Else
                    For y = before + first To after
                        .Items.Add(y)
                    Next
                End If
                If .Items.Count > 0 Then
                    If .Text = "" Then
                        .SelectedIndex = durability(equipment(x).Tag) - first
                    End If
                    .Show()
                Else
                    .Hide()
                    Calculate()
                End If
            End With
        Next
    End Sub

    Private Sub ShowDescription(sender As Object, e As EventArgs)
        If Not enemy_mode Then
            hover.SetToolTip(sender, description(sender.Tag))
        Else
            hover.SetToolTip(sender, E_description(sender.Tag))
        End If
    End Sub

    Private Sub ChangeAura(sender As Object, e As EventArgs)
        Dim chr As Integer = sender.Tag
        aura(chr, 0) = aura_type(chr).SelectedIndex
        If aura_type(chr).SelectedIndex = 0 Then
            aura_level(chr).Hide()
        Else
            aura_level(chr).Show()
        End If
        CheckAura(chr, True)
    End Sub

    Private Sub ChangeAuraLevel(sender As Object, e As EventArgs)
        Dim chr As Integer = sender.Tag
        aura(chr, 1) = aura_level(chr).SelectedIndex
        CheckAura(chr, True)
    End Sub

    Private Sub MoveButtonDown(sender As Object, e As EventArgs)
        If sender.Name < 3 Then
            sender.Top = button_row_1_Y_pos + 2
        Else
            sender.Top = button_row_2_Y_pos + 2
        End If
    End Sub

    Private Sub MoveButtonUp(sender As Object, e As EventArgs)
        sender.Hide()
        If sender.Name < 3 Then
            sender.Top = button_row_1_Y_pos
        Else
            sender.Top = button_row_2_Y_pos
        End If
        sender.Show()
    End Sub

    Private Sub ButtonClick(sender As Object, e As EventArgs)
        combo_results.Focus()
        Dim form() As Form = {Deck, QuestMagnus, Boost, Nothing, Nothing, Settings, MP, Target}
        Dim index As Integer = sender.Name
        Select Case index
            Case 3
                Dolphin()
            Case 4
                CopyTable()
            Case Else
                Dim window As Form = form(index)
                OpenWindow(window)
        End Select
    End Sub

    Private Sub OpenWindow(window As Form)
        If window.Visible Then
            window.Focus()
            window.WindowState = FormWindowState.Normal
            Return
        End If
        window.Show()
    End Sub

    Public Sub SelectWindow(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.X Then
            SwitchMode()
            Return
        End If
        Dim form() As Form = {Target, Deck, QuestMagnus, Boost, Settings, MP}
        Dim key() As Keys = {Keys.T, Keys.D, Keys.Q, Keys.B, Keys.S, Keys.M}
        Dim index As Integer = Array.IndexOf(key, e.KeyCode)
        If index = -1 Then
            Return
        End If
        Dim window As Form = form(index)
        If window Is sender Then
            window.Close()
        Else
            OpenWindow(window)
        End If
    End Sub

    Public Sub UpdateRows()
        Dim row As Integer
        For y = 0 To rows - 1
            If My.Settings.ResultsRow.ElementAt(y) = "1" Then
                row_pos(y) = row * 25
                row += 1
            End If
        Next
        For y = 0 To rows - 1
            Dim title As Label = table(0, y)
            If Not hit_modifier_row.Contains(y) Then
                If My.Settings.ResultsRow.ElementAt(y) = "0" Then
                    For x = 0 To hits
                        table(x, y).Hide()
                    Next
                    Continue For
                End If
                If title.Visible And title.Top = row_pos(y) + output_panel.AutoScrollPosition.Y Then
                    Continue For
                End If
                For x = 0 To hits
                    table(x, y).Top = row_pos(y) + output_panel.AutoScrollPosition.Y
                Next
                For x = 0 To hits
                    If Not output_panel.Contains(table(x, y)) Then
                        If x > 0 Then
                            table(x, y).Left = 104 + 52 * x + output_panel.AutoScrollPosition.X
                        Else
                            table(x, y).Left = 5 + output_panel.AutoScrollPosition.X
                        End If
                        output_panel.Controls.Add(table(x, y))
                    End If
                    table(x, y).Show()
                Next
                Continue For
            End If

            Dim i As Integer = Array.IndexOf(hit_modifier_row, y)
            If My.Settings.ResultsRow.ElementAt(y) = "0" Then
                title.Hide()
                For x = 1 To hits
                    hit_modifier(x, i).Hide()
                Next
                Continue For
            End If
            If title.Visible And title.Top = row_pos(y) + output_panel.AutoScrollPosition.Y Then
                Continue For
            End If
            title.Top = row_pos(y) + output_panel.AutoScrollPosition.Y
            title.Left = 5 + output_panel.AutoScrollPosition.X
            title.Show()
            For x = 1 To hits
                hit_modifier(x, i).Top = row_pos(y) + output_panel.AutoScrollPosition.Y
            Next
            For x = 1 To hits
                Dim box As ComboBox = hit_modifier(x, i)
                If Not output_panel.Contains(box) Then
                    box.Left = 104 + 52 * x + output_panel.AutoScrollPosition.X
                    output_panel.Controls.Add(box)
                    If Not knockdown_hit(x) Then
                        RemoveHandler box.SelectedIndexChanged, AddressOf ChangeDeviation
                        box.SelectedIndex = 3
                        AddHandler box.SelectedIndexChanged, AddressOf ChangeDeviation
                    End If
                End If
                box.Show()
            Next
        Next
        Clear(0)
    End Sub

    Private Sub CopyTable()
        If enemy_mode Then
            E_CopyTable()
            Return
        End If

        Dim output As String = ""
        Dim C As String = ""
        Dim prev As String
        Dim shield_limit As Integer = Math.Floor(true_max_HP * Me.shield_limit(combo_target)) - 1
        If My.Settings.EffectiveHPRemaining Then
            shield_limit += effective_max_HP - true_max_HP
        End If
        For y = 0 To rows - 1
            For x = 0 To hits
                prev = C
                C = NumberToColumn(x)

                'first column, variable names
                If x = 0 Then
                    output &= table(x, y).Text
                    If hits > 0 Then
                        output &= vbTab
                    End If
                    Continue For
                End If

                If knockdown_hit(x) Then
                    Select Case y
                        Case 0, 11, 17, 20, 28    'offense, quest magnus bonus, base defense, defense boost factor, crush output
                            output &= table(x, y).Text
                        Case 4      'offense deviation
                            output &= DeviationToText(x, 0)
                        Case 19     'crush status
                            output &= "=" & prev & "20+" & prev & "29"
                        Case 21     'defense deviation
                            output &= DeviationToText(x, 2)
                        Case 22     'total offense
                            output &= "=(" & C & "1+" & C & "12)*(1+0.01*" & C & "5)"
                        Case 24     'total defense
                            output &= "=" & C & "18*" & C & "21*(1+0.01*" & C & "22)"
                        Case 27     'damage output
                            output &= "=trunc(min(" & C & "23;" & C & "25))"
                        Case 29     'total damage output
                            output &= "=sum($B28:" & C & "28)"
                        Case 30     'total crush output
                            output &= "=sum($B29:" & C & "29)"
                        Case 31     'HP remaining
                            output &= "=max(0;" & prev & "32-" & C & "28)"
                    End Select
                    If x < hits Then
                        output &= vbTab
                    End If
                    Continue For
                End If

                Select Case y
                    Case 4      'offense deviation
                        output &= DeviationToText(x, 0)
                    Case 5      'crush deviation
                        output &= DeviationToText(x, 1)
                    Case 17     'base defense
                        If shield.Visible And shield.Checked Then
                            If x = 1 Then
                                output &= defense(combo_target, hit_element(x))
                            Else
                                output &= "=if(" & prev & "32>" & shield_limit & ";" & defense(combo_target, hit_element(x)) & ";" & Math.Floor(defense(combo_target, hit_element(x)) * 0.8) & ")"
                            End If
                        Else
                            output &= table(x, y).Text
                        End If
                    Case 18     'crush limit
                        If shield.Visible And shield.Checked Then
                            If x = 1 Then
                                output &= crush_limit(combo_target)
                            Else
                                output &= "=if(" & prev & "32>" & shield_limit & ";" & crush_limit(combo_target) & ";" & Math.Floor(crush_limit(combo_target) * 0.8) & ")"
                            End If
                        Else
                            output &= table(x, y).Text
                        End If
                    Case 19     'crush status
                        If x = 1 Then
                            output &= "0"
                        Else
                            output &= "=" & prev & "20+" & prev & "29"
                        End If
                    Case 21     'defense deviation
                        output &= DeviationToText(x, 2)
                    Case 22     'total offense
                        output &= "=(" & C & "1*" & C & "2*" & C & "4*(1+0.01*" & C & "5)*" & C & "7+(" & C & "8*" & C & "10*" & C & "11)+" & C & "12+" & C & "13)*" & C & "15*" & C & "17"
                    Case 23     'total crush
                        If table(x, 2).Text = "" Then      'Regalia hit
                            Exit Select
                        End If
                        output &= "=(" & C & "1*" & C & "3*" & C & "4*(1+0.01*" & C & "6)*" & C & "7+(" & C & "9*" & C & "10*" & C & "11)+" & C & "12+" & C & "14)*" & C & "16*" & C & "17"
                    Case 24     'total defense
                        output &= "=" & C & "18*max(0;1-" & C & "20/" & C & "19)*" & C & "21*(1+0.01*" & C & "22)"
                    Case 27     'damage output
                        If min_one(x) And table(x, 25).Text = "" And table(x, 26).Text = "" Then
                            output &= "=max(1;"
                        Else
                            output &= "=max(0;"
                        End If
                        output &= "trunc(if(" & C & "23>" & C & "25+" & C & "26;(" & C & "23-0.775*" & C & "25-" & C & "26)*" & C & "27;(" & C & "23-0.1*" & C & "25-" & C & "26)*0.25*" & C & "27)))"
                    Case 28     'crush output
                        If table(x, 2).Text = "50" AndAlso combo(hit_card(x)).Tag = 10 Then   'Rabbit Dash 2nd hit
                            If shield.Visible And shield.Checked Then
                                output &= "=max(0;" & "if(" & prev & "32>" & shield_limit & ";" & knockdown(combo_target) & ";" & Math.Floor(knockdown(combo_target) * 0.8) & ")-" & C & "20" & ";"
                            ElseIf shield.Visible Then
                                output &= "=max(0;" & Math.Floor(knockdown(combo_target) * 0.8) & "-" & C & "20" & ";"
                            Else
                                output &= "=max(0;" & knockdown(combo_target) & "-" & C & "20" & ";"
                            End If
                        Else
                            output &= "=max(0;"
                        End If
                        output &= "if(" & C & "24>0.5*" & C & "25+" & C & "26;(" & C & "24-0.3875*" & C & "25-" & C & "26)*" & C & "27;(" & C & "24-0.05*" & C & "25-" & C & "26)*0.25*" & C & "27))"
                    Case 29     'total damage output
                        output &= "=sum($B28:" & C & "28)"
                    Case 30     'total crush output
                        output &= "=sum($B29:" & C & "29)"
                    Case 31     'HP remaining
                        If x = 1 Then
                            If My.Settings.EffectiveHPRemaining And target_data(7).Visible Then
                                output &= "=max(0;" & effective_HP & "-" & C & "28)"
                            Else
                                output &= "=max(0;" & true_HP & "-" & C & "28)"
                            End If
                        Else
                            output &= "=max(0;" & prev & "32-" & C & "28)"
                        End If
                    Case 3, 6, 10, 14, 15, 16, 20, 26       'factors
                        If table(x, y).Text <> "" Then
                            output &= table(x, y).Text
                        Else
                            output &= "1"
                        End If
                    Case Else
                        output &= table(x, y).Text
                End Select
                If x < hits Then
                    output &= vbTab
                End If
            Next
            If y < 31 Then
                output &= vbCrLf
            End If
        Next
        Clipboard.SetText(output)
    End Sub

    Private Function NumberToColumn(n As Integer) As String
        Dim output As String = ""
        Do
            If output.Length > 0 Then
                n -= 1
            End If
            output = Convert.ToChar(n Mod 26 + 65) & output
            n = Math.Floor(n / 26)
        Loop Until n = 0
        Return output
    End Function

    Private Function DeviationToText(x As Integer, y As Integer) As String
        Dim zero_index As Integer
        If hit_modifier(x, y).Items.Count = 8 Then
            zero_index = 3
        Else
            zero_index = 4
        End If
        Select Case hit_modifier(x, y).SelectedIndex
            Case -1, zero_index
                Return ""
            Case Else
                Return zero_index - hit_modifier(x, y).SelectedIndex
        End Select
    End Function

    Private Function DeviationToNumber(x As Integer, y As Integer) As Integer
        Dim zero_index As Integer
        If hit_modifier(x, y).Items.Count = 8 Then
            zero_index = 3
        Else
            zero_index = 4
        End If
        If hit_modifier(x, y).SelectedIndex = -1 Then
            Return 0
        Else
            Return zero_index - hit_modifier(x, y).SelectedIndex
        End If
    End Function

    Public Sub GetPartyData()
        If Not enemy_mode Then
            Return
        End If
        Dim chr As Integer = character - 1
        Dim level As Integer = Me.level(chr)
        If level <= 50 Then
            member_max_hp = party_HP(chr, level)
            member_crush_limit = party_crush_limit(chr, level)
            member_knockdown = party_knockdown(chr, level)
            member_knockout = party_knockout(chr, level)
            For x = 0 To 5
                member_defense(x) = party_defense(chr, level, x)
            Next
        Else
            Dim lv50, lv100 As Integer
            Dim factor As Double = level / 50 - 1

            lv50 = party_HP(chr, 50)
            lv100 = party_HP(chr, 51)
            member_max_hp = lv50 + Math.Floor(Math.Round((lv100 - lv50) * factor, 2))

            lv50 = party_crush_limit(chr, 50)
            lv100 = party_crush_limit(chr, 51)
            member_crush_limit = lv50 + Math.Floor(Math.Round((lv100 - lv50) * factor, 2))

            lv50 = party_knockdown(chr, 50)
            lv100 = party_knockdown(chr, 51)
            member_knockdown = lv50 + Math.Floor(Math.Round((lv100 - lv50) * factor, 2))

            lv50 = party_knockout(chr, 50)
            lv100 = party_knockout(chr, 51)
            member_knockout = lv50 + Math.Floor(Math.Round((lv100 - lv50) * factor, 2))

            For x = 0 To 5
                lv50 = party_defense(chr, 50, x)
                lv100 = party_defense(chr, 51, x)
                member_defense(x) = lv50 + Math.Floor(Math.Round((lv100 - lv50) * factor, 2))
            Next
        End If
        Dim armor_bonus As Integer
        Dim eq As Integer = equipment(chr).Tag
        If eq <> 0 Then
            armor_bonus = armor_crush_limit(eq)
        End If

        member_max_hp += Math.Floor(Math.Round(member_max_hp * 0.01 * QM_total_bonus(character + 11), 2))
        member_knockdown += Math.Floor(Math.Round(member_knockdown * 0.01 * QM_total_bonus(10), 2))
        member_knockout += Math.Floor(Math.Round(member_knockout * 0.01 * QM_total_bonus(11), 2))
        member_max_hp = Math.Max(1, member_max_hp)
        If eq = 256 Then
            member_max_hp_armor = Math.Floor(Math.Round(member_max_hp * 1.2, 2)) + aura_HP(chr)
        Else
            member_max_hp_armor = member_max_hp + aura_HP(chr)
        End If
        member_knockdown_armor = member_knockdown * (1 + 0.01 * armor_bonus) + aura_down(chr)
        member_knockout_armor = member_knockout * (1 + 0.01 * armor_bonus) + aura_down(chr)
        member_max_hp += aura_HP(chr)
        member_knockdown += aura_down(chr)
        member_knockout += aura_down(chr)
        If E_target_data(1).Text <> member_max_hp_armor.ToString Then
            member_hp = member_max_hp_armor
            E_HP_box.Text = member_max_hp_armor
            E_target_data(1).Text = member_max_hp_armor
        End If
        E_target_data(3).Text = Decimals(member_knockdown_armor)
        E_target_data(5).Text = Decimals(member_knockout_armor)
    End Sub

    Private Sub E_SwitchTarget()
        Dim index As Integer = My.Settings.PartyOrder.IndexOf(character - 1)
        If index = 2 Then
            index = 0
        Else
            index += 1
        End If
        character = My.Settings.PartyOrder.Substring(index, 1) + 1
        SwitchCharacter(char_image(character - 1), New EventArgs)
    End Sub

    Private Sub E_AddCard(sender As Object, e As MouseEventArgs)
        sender.Focus()
        If cards = 6 Then
            Return
        End If
        Dim card As Integer = sender.Name
        If E_hand(card).Cursor = Cursors.Default Then
            Return
        End If
        Dim attack As Integer = sender.Tag

        'add card to combo
        With combo(cards)
            .Image = sender.Image
            .Tag = attack
            .AccessibleName = sender.AccessibleName
            If Not card_panel(1).Contains(combo(cards)) Then
                '.Left += card_panel(1).AutoScrollPosition.X
                card_panel(1).Controls.Add(combo(cards))
            End If
            .Show()
        End With

        cards += 1
        E_ShowDeck()
        E_Calculate()
    End Sub

    Private Sub E_ShowDeck()
        If Not enemy_mode Then
            Return
        End If
        Dim transparent As Boolean
        If cards = 6 Then
            transparent = True
        End If
        If cards > 0 AndAlso combo(cards - 1).AccessibleName > 10 Then
            transparent = True
        End If
        For x = 0 To 5
            Dim animation, normal, special, id As Integer
            animation = enemy_attack_data(combo_target, x, 0)
            If animation = 0 Then
                E_hand(x).Hide()
                Continue For
            End If

            Dim bmp As Bitmap
            If combo_target < 101 AndAlso (animation >= 200 And animation < 1000) Then
                animation = 1000
            End If
            If animation < 1000 Then
                normal += 1
                id = normal
            Else
                special += 1
                id = special + 10
            End If
            If id < 10 Then
                bmp = magnus_image(id)
            Else
                bmp = New Bitmap(My.Resources.ResourceManager.GetObject("special_" & id - 10), New Size(50, 80))
            End If
            If Not transparent Then
                E_hand(x).Image = bmp
                E_hand(x).Cursor = Cursors.Hand
            Else
                E_hand(x).Image = MakeTransparent(bmp)
                E_hand(x).Cursor = Cursors.Default
            End If

            E_hand(x).Tag = x
            E_hand(x).AccessibleName = id
            E_hand(x).Show()
        Next
    End Sub

    Private Sub E_ShowAttackName(sender As Object, e As EventArgs)
        Dim name As String = enemy_attack_name(combo_target, sender.Tag)
        hover.SetToolTip(sender, name)
        If name <> "" Then
        End If
    End Sub

    Private Sub E_HighlightHits(sender As Object, e As EventArgs)
        If Not My.Settings.HighlightHits Then
            Return
        End If
        Dim card As Integer = sender.Name
        For x = first_hit(card) To 59
            If hit_card(x) <> card Then
                Exit For
            End If
            For y = 0 To 25
                If Not E_hit_modifier_row.Contains(y) AndAlso E_table(x, y).BackColor = default_color Then
                    E_table(x, y).BackColor = Color.LightBlue
                End If
            Next
        Next
        highlight = card
    End Sub

    Private Sub E_UnhighlightHits(sender As Object, e As EventArgs)
        highlight = -1
        Dim card As Integer = sender.Name
        For x = first_hit(card) To 59
            If hit_card(x) <> card Then
                Exit For
            End If
            For y = 0 To 25
                If Not E_hit_modifier_row.Contains(y) AndAlso E_table(x, y).BackColor = Color.LightBlue Then
                    E_table(x, y).BackColor = default_color
                End If
            Next
        Next
    End Sub

    Private Sub E_Clear(hits_prev As Integer)
        For x = hits + 1 To hits_prev
            For y = 0 To 25
                If Not E_hit_modifier_row.Contains(y) Then
                    E_table(x, y).Hide()
                End If
            Next
            For y = 0 To 2
                E_hit_modifier(x, y).Hide()
                E_hit_modifier(x, y).SelectedIndex = Math.Floor((E_hit_modifier(x, y).Items.Count - 1) * 0.5)
            Next
            knockdown_hit(x) = False
            prevent_crit(x) = False
            min_one(x) = False
            hit_card(x) = -1
        Next

        Dim highlight(5) As Boolean
        For x = 1 To hits
            If E_hit_modifier(x, 0).SelectedIndex <> Math.Floor((E_hit_modifier(x, 0).Items.Count - 1) * 0.5) Then
                highlight(0) = True
                Exit For
            End If
        Next
        For x = 1 To hits
            If E_hit_modifier(x, 1).SelectedIndex <> 3 Then
                highlight(1) = True
                Exit For
            End If
        Next
        If Array.IndexOf(prevent_crit, True, 1, hits) >= 0 Then
            highlight(2) = True
        End If
        For x = 1 To hits
            If E_hit_modifier(x, 2).SelectedIndex <> Math.Floor((E_hit_modifier(x, 2).Items.Count - 1) * 0.5) Then
                highlight(3) = True
                Exit For
            End If
        Next
        If Array.IndexOf(min_one, True, 1, hits) >= 0 Then
            highlight(4) = True
        End If
        For x = 0 To 4
            If highlight(x) Then
                E_table(0, E_clickable_rows(x)).BackColor = Color.LightYellow
            Else
                E_table(0, E_clickable_rows(x)).BackColor = default_color
            End If
        Next
    End Sub

    Private Sub E_ShowHitModifiers()
        Dim hit As Integer = hits + 1

        For i = 0 To 2
            If My.Settings.EnemyResultsRow.ElementAt(E_hit_modifier_row(i)) = "0" Then     'row hidden by user
                Continue For
            End If
            With E_hit_modifier(hit, i)
                .Top = E_row_pos(E_hit_modifier_row(i)) + E_output_panel.AutoScrollPosition.Y
                If Not E_output_panel.Contains(E_hit_modifier(hit, i)) Then
                    .Left = 104 + 52 * hit + E_output_panel.AutoScrollPosition.X
                    E_output_panel.Controls.Add(E_hit_modifier(hit, i))
                    .SelectedIndex = 3
                End If
                .Show()
            End With
        Next

        Dim offense As ComboBox = E_hit_modifier(hit, 0)
        Dim crush As ComboBox = E_hit_modifier(hit, 1)
        Dim defense As ComboBox = E_hit_modifier(hit, 2)

        'on fire/dark knockdown extra hits, offense/defense deviation ranges from -20% to +4%
        If knockdown_hit(hit) And offense.Items.Count < 25 Then
            offense.Items.Clear()
            defense.Items.Clear()
            For i = 4 To 1 Step -1
                offense.Items.Add("+" & i & "%")
                defense.Items.Add("+" & i & "%")
            Next
            For i = 0 To -20 Step -1
                offense.Items.Add(i & "%")
                defense.Items.Add(i & "%")
            Next
            offense.SelectedIndex = 12
            defense.SelectedIndex = 12
            crush.SelectedIndex = 3
        ElseIf Not knockdown_hit(hit) And offense.Items.Count >= 25 Then
            offense.Items.Clear()
            defense.Items.Clear()
            For i = 3 To 1 Step -1
                offense.Items.Add("+" & i & "%")
                defense.Items.Add("+" & i & "%")
            Next
            For i = 0 To -4 Step -1
                offense.Items.Add(i & "%")
                defense.Items.Add(i & "%")
            Next
            offense.SelectedIndex = 3
            defense.SelectedIndex = 3
            crush.SelectedIndex = 3
        End If
    End Sub

    Private Sub E_NewHit()
        For y = 0 To 25
            With E_table(hits, y)
                If Not E_hit_modifier_row.Contains(y) Then
                    .Text = ""
                    If highlight = hit_card(hits) Then
                        .BackColor = Color.LightBlue
                    ElseIf .BackColor <> default_color Then
                        .BackColor = default_color
                    End If
                End If

                If My.Settings.EnemyResultsRow.ElementAt(y) = "0" Or E_hit_modifier_row.Contains(y) Then
                    Continue For
                End If

                .Top = E_row_pos(y) + E_output_panel.AutoScrollPosition.Y
                If Not E_output_panel.Contains(E_table(hits, y)) Then
                    .Left = 104 + 52 * hits + E_output_panel.AutoScrollPosition.X
                    E_output_panel.Controls.Add(E_table(hits, y))
                End If
                .Show()
            End With
        Next
    End Sub

    Private Sub E_ShowHit(offense As Double, crush As Double, attack_offense As Double, attack_crush As Double, attack_boost_factor As Double, armor_offense As Integer, crit_factor As Double, status As Integer, base_defense As Integer, qm_defense As Integer, max_crush As Double, crush_status As Double, defense_boost_factor As Double, armor_factor As Double, aura_defense As Integer, total_offense As Double, total_crush As Double, total_defense As Double, armor_defense As Integer, damage_output As Integer, crush_output As Double, total_damage As Integer, HP_remaining As Integer, attack_element As Integer, knock_down As Double, knock_out As Double)
        E_NewHit()

        E_table(hits, 0).Text = offense
        E_table(hits, 1).Text = crush
        E_table(hits, 2).Text = attack_offense
        E_table(hits, 3).Text = attack_crush
        If attack_boost_factor <> 1 Then
            E_table(hits, 4).Text = Round(attack_boost_factor)
        End If
        If armor_offense <> 0 Then
            E_table(hits, 7).Text = armor_offense
        End If

        If crit_factor <> 1 Then
            E_table(hits, 8).Text = crit_factor
        End If
        If status <> 0 Then
            If status = 4 Then
                E_table(hits, 8).BackColor = element_color(5)
            Else
                E_table(hits, 8).BackColor = element_color(status)
            End If
        End If

        E_table(hits, 9).Text = base_defense
        If qm_defense <> 0 Then
            E_table(hits, 10).Text = qm_defense
        End If
        E_table(hits, 11).Text = Decimals(max_crush)
        E_table(hits, 12).Text = Decimals(crush_status)
        If defense_boost_factor <> 1 Then
            E_table(hits, 13).Text = Round(defense_boost_factor)
        End If
        If armor_factor <> 1 Then
            E_table(hits, 15).Text = armor_factor
        End If

        If aura_defense <> 0 Then
            E_table(hits, 16).Text = aura_defense
        End If

        E_table(hits, 17).Text = Decimals(total_offense)
        E_table(hits, 18).Text = Decimals(total_crush)
        E_table(hits, 19).Text = Decimals(total_defense)

        If armor_defense <> 0 Then
            E_table(hits, 20).Text = armor_defense
        End If

        E_table(hits, 21).Text = damage_output
        E_table(hits, 22).Text = Decimals(crush_output)

        E_table(hits, 23).Text = total_damage
        Dim total_crush_output As Double = crush_status + crush_output
        E_table(hits, 24).Text = Decimals(total_crush_output)
        E_table(hits, 25).Text = HP_remaining

        E_table(hits, 21).BackColor = element_color(attack_element)
        E_table(hits, 22).BackColor = element_color(attack_element)

        If total_crush_output >= knock_out And knock_out > 0 Then
            E_table(hits, 24).BackColor = Color.Orange
        ElseIf total_crush_output >= knock_down And knock_down > 0 Then
            E_table(hits, 24).BackColor = Color.Yellow
        End If
    End Sub

    Private Sub E_ShowKnockdownHit(offense As Integer, status As Integer, base_defense As Integer, total_offense As Double, total_defense As Double, damage_output As Integer, total_damage As Integer, HP_remaining As Integer, attack_element As Integer)
        E_NewHit()

        E_table(hits, 0).Text = offense

        If status <> 0 Then
            If status = 4 Then
                E_table(hits, 8).BackColor = element_color(5)
            Else
                E_table(hits, 8).BackColor = element_color(status)
            End If
        End If

        E_table(hits, 9).Text = base_defense
        E_table(hits, 13).Text = "0.1"

        E_table(hits, 17).Text = Decimals(total_offense)
        E_table(hits, 19).Text = Decimals(total_defense)

        E_table(hits, 21).Text = damage_output
        E_table(hits, 22).Text = 0
        E_table(hits, 23).Text = total_damage
        E_table(hits, 24).Text = E_table(hits - 1, 24).Text
        E_table(hits, 25).Text = HP_remaining
        E_table(hits, 21).BackColor = element_color(attack_element)
        E_table(hits, 22).BackColor = element_color(attack_element)

        Dim change_color() As Integer = {5, 6, 8, 14, 21, 22}
        For y = 0 To 25
            If Not E_hit_modifier_row.Contains(y) And Not change_color.Contains(y) Then
                E_table(hits, y).BackColor = E_table(hits - 1, y).BackColor
            End If
        Next
    End Sub

    Private Sub E_ToggleEffect(sender As Object, e As EventArgs)
        Dim hit As Integer = sender.Tag
        Select Case sender.Name
            Case 8
                prevent_crit(hit) = Not prevent_crit(hit)
            Case 21
                min_one(hit) = Not min_one(hit)
        End Select
        E_Calculate()
    End Sub

    Private Sub E_ResetRow(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Middle Then
            Return
        End If
        Dim row As Integer = sender.Tag
        Select Case row
            Case 5                  'reset offense deviation
                For x = 1 To hits
                    E_hit_modifier(x, 0).SelectedIndex = Math.Floor((E_hit_modifier(x, 0).Items.Count - 1) * 0.5)
                Next
            Case 6                  'reset crush deviation
                For x = 1 To hits
                    E_hit_modifier(x, 1).SelectedIndex = 3
                Next
            Case 8                  'remove all critical hit blocks
                For x = 1 To hits
                    prevent_crit(x) = False
                Next
            Case 14                 'reset defense deviation
                For x = 1 To hits
                    E_hit_modifier(x, 2).SelectedIndex = Math.Floor((E_hit_modifier(x, 2).Items.Count - 1) * 0.5)
                Next
            Case 21                 'remove all "minimum 1" hits
                For x = 1 To hits
                    min_one(x) = False
                Next
            Case Else
                Return
        End Select
        E_table(0, row).BackColor = default_color
        E_Calculate()
    End Sub

    Private Sub E_HideRow(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Middle Then
            Return
        End If
        Dim i As Integer = sender.Tag
        Dim temp As String = My.Settings.EnemyResultsRow
        temp = temp.Remove(i, 1)
        temp = temp.Insert(i, "0")
        My.Settings.EnemyResultsRow = temp
        E_UpdateRows()
        If Settings.Visible Then
            RemoveHandler Settings.enemy_row(i).CheckedChanged, AddressOf Settings.E_ToggleRow
            Settings.enemy_row(i).Checked = False
            Settings.enemy_row(i).BackColor = default_color
            AddHandler Settings.enemy_row(i).CheckedChanged, AddressOf Settings.E_ToggleRow
        End If
    End Sub

    Public Sub E_UpdateRows()
        Dim row As Integer
        For y = 0 To 25
            If My.Settings.EnemyResultsRow.ElementAt(y) = "1" Then
                E_row_pos(y) = row * 25
                row += 1
            End If
        Next
        For y = 0 To 25
            Dim title As Label = E_table(0, y)
            If Not E_hit_modifier_row.Contains(y) Then
                If My.Settings.EnemyResultsRow.ElementAt(y) = "0" Then
                    For x = 0 To hits
                        E_table(x, y).Hide()
                    Next
                    Continue For
                End If
                If title.Visible And title.Top = E_row_pos(y) + E_output_panel.AutoScrollPosition.Y Then
                    Continue For
                End If
                For x = 0 To hits
                    E_table(x, y).Top = E_row_pos(y) + E_output_panel.AutoScrollPosition.Y
                Next
                For x = 0 To hits
                    If Not E_output_panel.Contains(E_table(x, y)) Then
                        If x > 0 Then
                            E_table(x, y).Left = 104 + 52 * x + E_output_panel.AutoScrollPosition.X
                        Else
                            E_table(x, y).Left = 5 + E_output_panel.AutoScrollPosition.X
                        End If
                        E_output_panel.Controls.Add(E_table(x, y))
                    End If
                    E_table(x, y).Show()
                Next
                Continue For
            End If

            Dim i As Integer = Array.IndexOf(E_hit_modifier_row, y)
            If My.Settings.EnemyResultsRow.ElementAt(y) = "0" Then
                title.Hide()
                For x = 1 To hits
                    E_hit_modifier(x, i).Hide()
                Next
                Continue For
            End If
            If title.Visible And title.Top = E_row_pos(y) + E_output_panel.AutoScrollPosition.Y Then
                Continue For
            End If
            title.Top = E_row_pos(y) + E_output_panel.AutoScrollPosition.Y
            title.Left = 5 + E_output_panel.AutoScrollPosition.X
            title.Show()
            For x = 1 To hits
                E_hit_modifier(x, i).Top = E_row_pos(y) + E_output_panel.AutoScrollPosition.Y
            Next
            For x = 1 To hits
                Dim box As ComboBox = E_hit_modifier(x, i)
                If Not E_output_panel.Contains(box) Then
                    box.Left = 104 + 52 * x + E_output_panel.AutoScrollPosition.X
                    E_output_panel.Controls.Add(box)
                    If Not knockdown_hit(x) Then
                        RemoveHandler box.SelectedIndexChanged, AddressOf ChangeDeviation
                        box.SelectedIndex = 3
                        AddHandler box.SelectedIndexChanged, AddressOf ChangeDeviation
                    End If
                End If
                box.Show()
            Next
        Next
        Clear(0)
    End Sub

    Private Sub E_CopyTable()
        Dim output As String = ""
        Dim C As String = ""
        Dim prev As String
        For y = 0 To 25
            For x = 0 To hits
                prev = C
                C = NumberToColumn(x)

                'first column, variable names
                If x = 0 Then
                    output &= E_table(x, y).Text
                    If hits > 0 Then
                        output &= vbTab
                    End If
                    Continue For
                End If

                If knockdown_hit(x) Then
                    Select Case y
                        Case 0, 9, 13, 22   'offense, base defense, defense boost factor, crush output
                            output &= E_table(x, y).Text
                        Case 5              'offense deviation
                            output &= E_DeviationToText(x, 0)
                        Case 12             'crush status
                            output &= "=" & prev & "13+" & prev & "23"
                        Case 14             'defense deviation
                            output &= E_DeviationToText(x, 2)
                        Case 17             'total offense
                            output &= "=" & C & "1*(1+0.01*" & C & "6)"
                        Case 19             'total defense
                            output &= "=" & C & "10*" & C & "14*(1+0.01*" & C & "15)"
                        Case 21             'damage output
                            output &= "=trunc(min(" & C & "18;" & C & "20))"
                        Case 23             'total damage output
                            output &= "=sum($B22:" & C & "22)"
                        Case 24             'total crush output
                            output &= "=sum($B23:" & C & "23)"
                        Case 25             'HP remaining
                            output &= "=max(0;" & prev & "26-" & C & "22)"
                    End Select
                    If x < hits Then
                        output &= vbTab
                    End If
                    Continue For
                End If

                Select Case y
                    Case 5      'offense deviation
                        output &= E_DeviationToText(x, 0)
                    Case 6      'crush deviation
                        output &= E_DeviationToText(x, 1)
                    Case 12     'crush status
                        If x = 1 Then
                            output &= "0"
                        Else
                            output &= "=" & prev & "13+" & prev & "23"
                        End If
                    Case 14     'defense deviation
                        output &= E_DeviationToText(x, 2)
                    Case 17     'total offense
                        output &= "=(" & C & "1*" & C & "3*" & C & "5*(1+0.01*" & C & "6)+" & C & "8)*" & C & "9"
                    Case 18     'total crush
                        output &= "=(" & C & "2*" & C & "4*" & C & "5*(1+0.01*" & C & "7)+" & C & "8)*" & C & "9"
                    Case 19     'total defense
                        output &= "=(" & C & "10+" & C & "11)*max(0;1-" & C & "13/" & C & "12)*" & C & "14*(1+0.01*" & C & "15)*" & C & "16+" & C & "17"
                    Case 21     'damage output
                        If min_one(x) Then
                            output &= "=max(1;"
                        Else
                            output &= "=max(0;"
                        End If
                        output &= "trunc(if(" & C & "18>" & C & "20+" & C & "21;" & C & "18-0.775*" & C & "20-" & C & "21;(" & C & "18-0.1*" & C & "20-" & C & "21)*0.25)))"
                    Case 22     'crush output
                        output &= "=max(0;if(" & C & "19>0.5*" & C & "20+" & C & "21;" & C & "19-0.3875*" & C & "20-" & C & "21;(" & C & "19-0.05*" & C & "20-" & C & "21)*0.25))"
                    Case 23     'total damage output
                        output &= "=sum($B22:" & C & "22)"
                    Case 24     'total crush output
                        output &= "=sum($B23:" & C & "23)"
                    Case 25     'HP remaining
                        If x = 1 Then
                            output &= "=max(0;" & member_hp & "-" & C & "22)"
                        Else
                            output &= "=max(0;" & prev & "26-" & C & "22)"
                        End If
                    Case 4, 8, 13, 15       'factors
                        If E_table(x, y).Text <> "" Then
                            output &= E_table(x, y).Text
                        Else
                            output &= "1"
                        End If
                    Case Else
                        output &= E_table(x, y).Text
                End Select
                If x < hits Then
                    output &= vbTab
                End If
            Next
            If y < 25 Then
                output &= vbCrLf
            End If
        Next
        Clipboard.SetText(output)
    End Sub

    Private Function E_DeviationToText(x As Integer, y As Integer) As String
        Dim zero_index As Integer
        If E_hit_modifier(x, y).Items.Count = 8 Then
            zero_index = 3
        Else
            zero_index = 4
        End If
        Select Case E_hit_modifier(x, y).SelectedIndex
            Case -1, zero_index
                Return ""
            Case Else
                Return zero_index - E_hit_modifier(x, y).SelectedIndex
        End Select
    End Function

    Private Function E_DeviationToNumber(x As Integer, y As Integer) As Integer
        Dim zero_index As Integer
        If E_hit_modifier(x, y).Items.Count = 8 Then
            zero_index = 3
        Else
            zero_index = 4
        End If
        If E_hit_modifier(x, y).SelectedIndex = -1 Then
            Return 0
        Else
            Return zero_index - E_hit_modifier(x, y).SelectedIndex
        End If
    End Function
End Class