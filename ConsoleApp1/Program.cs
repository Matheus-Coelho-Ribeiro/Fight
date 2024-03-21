using System;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;



class CreatePlayer {
    
    //snow in the console the attibute that the player will place and the min and max of it

    public void TextMin(int minP, int maxP, string statusEdit, int playerSelect)
    {
        Console.WriteLine($"minimun and maximu number of points you can use : {minP} - {maxP}");
        Console.WriteLine($"choose the amount of {statusEdit} for player {playerSelect}");
    }



    //will return the players' attributes in an array

    public int[] ReturnPlayerStatus(string[] statusInfo, int[] minStatus, int[] maxStatus, int maxPoints, int player)
    {
        int[] willReturn = new int[8] {0, 0, 0, 0, 0, 0, 0, 0}; //array return attributes

        for(int i = 0; i <= 7; i++) //loop to get each value to avoid errors
        {
            int maxRT;
            if(maxPoints > 0) 
            {   
                Console.WriteLine("\n------------------------------------------------");
                Console.WriteLine($"+ you have {maxPoints} points left");
                this.TextMin(minStatus[i], maxStatus[i], statusInfo[i], player);
                Console.WriteLine("------------------------------------------------\n\n");
                string getValue = Console.ReadLine();
                if(!int.TryParse(getValue, out int valueStatusInt))
                {
                    Console.WriteLine("Invalid value, enter a number");
                    i--;
                    continue;
                }
                else 
                {
                    if(valueStatusInt <= (minStatus[i] - 1) || valueStatusInt > maxStatus[i]) 
                    {
                        Console.WriteLine($"Enter a valid number from {minStatus[i]} - {maxStatus[i]}");
                        i--;
                        continue;
                    }

                    if(maxPoints < valueStatusInt) {
                        Console.WriteLine($"insufficient points, you only have {maxPoints} points");
                        i--;
                        continue;
                    }
                    else 
                    {
                        willReturn[i] = valueStatusInt;
                        maxPoints -= valueStatusInt;
                    }
                }
            }
            else 
            {
                Console.WriteLine("------------------------------------------------");
                TextMin(minStatus[i], maxStatus[i], statusInfo[i], player);
                Console.WriteLine("no points to spend");
            }
        }
        
        
        string typeDamage = ""; //a way to get the player's damage type

        if(willReturn[1] > willReturn[4] && willReturn[4] == 0)
        {
            typeDamage = "PHYSICAL";
        } 
        else if(willReturn[1] < willReturn[4] && willReturn[1] == 0)
        {
            typeDamage = "MAGIC";
        } 
        else if(willReturn[1] > 0 && willReturn[4] > 0)
        {
            typeDamage = "HYBRID";
        }
        else 
        {
            typeDamage = "HYBRID";

        }


        if(maxPoints > 0) //assigning points if points remain that have not been used
        {
            if(typeDamage == "PHYSICAL") 
            {
                for(int i = 0; i <= 3; i++)
                {
                    if(maxPoints > 0) 
                    {
                        int varOfStatus = maxStatus[i] - willReturn[i];
                        if(varOfStatus < maxPoints) {
                            Console.WriteLine($"{varOfStatus} {statusInfo[i]} points were distributed (player {player})");
                            willReturn[i] += varOfStatus;
                            maxPoints -= varOfStatus;
                        }
                        else 
                        {
                            Console.WriteLine($"{maxPoints} {statusInfo[i]} points were distributed (player {player})");
                            willReturn[i] += maxPoints;
                            maxPoints = 0;
                        }
                    }   
                }
            }
            else if (typeDamage == "MAGIC") 
            {
                int maxR;
                for(int i = 0; i <= 2; i++)
                {
                    if(i == 0)
                    {
                        maxR = 20;
                        if(maxPoints > 0)
                        {
                            int varOfStatus = maxR - willReturn[i];
                            if(varOfStatus < maxPoints) 
                            {
                                Console.WriteLine($"{varOfStatus} {statusInfo[i]} points were distributed (player {player})");
                                willReturn[i] += varOfStatus;
                                maxPoints -= varOfStatus;
                            }
                            else
                            {
                                Console.WriteLine($"{maxPoints} {statusInfo[i]} points were distributed (player {player})");
                                willReturn[i] += maxPoints;
                                maxPoints = 0;
                            }
                        }
                    }
                    else 
                    {
                        maxR = 24;
                        if(maxPoints > 0)
                        {
                            int varOfStatus = maxR - willReturn[i + 3];
                            if(varOfStatus < maxPoints) 
                            {
                                Console.WriteLine($"{varOfStatus} {statusInfo[i + 3]} points were distributed (player {player})");
                                willReturn[i] += varOfStatus;
                                maxPoints -= varOfStatus;
                            }
                            else
                            {
                                Console.WriteLine($"{maxPoints} {statusInfo[i + 3]} points were distributed  (player {player})");
                                willReturn[i] += maxPoints;
                                maxPoints = 0;
                            }
                        }
                    }

                }
            }
            else if(typeDamage == "HYBRID")
            {
                for(int i = 0; i <= 5; i++)
                {
                    if(maxPoints > 0) 
                    {
                        int varOfStatus = maxStatus[i] - willReturn[i];
                        if(varOfStatus < maxPoints) {
                            Console.WriteLine($"{varOfStatus} {statusInfo[i]} points were distributed  (player {player})");
                            willReturn[i] += varOfStatus;
                            maxPoints -= varOfStatus;
                        }
                        else 
                        {
                            Console.WriteLine($"{maxPoints} {statusInfo[i]} points were distributed  (player {player})");
                            willReturn[i] += maxPoints;
                            maxPoints = 0;
                        }
                    }
                }
            }
                
        }

        return willReturn;
    }

    
}

//attributes in array (if you have any questions)
//-----------------------------------------------
//(0) life
//(1) atkPhysical
//(2) critical chance
//(3) critical damage
//(4) magic attack
//(5) magic penetration
//(6) physical resistence
//(7) magic resistente
//-----------------------------------------------


//Player creation (BASE)
class PlayerRPG {
    //Basic attributes
    public string name;
    public double life;
    public double atkPhysical;
    public double criticalChance;
    public double criticalDamage;
    public double atkMagic;
    public double magicPenetration;
    public double resistencePhysical;
    public double resistenceMagic;

    public string typeDamage;
    public int specialAttack;

    public PlayerRPG(string name, double life, int atkPhysical, int criticalChance, int criticalDamage, double atkMagic, int magicPenetration, int resistencePhysical, int resistenceMagic)
    {
        this.name = name;                                  //name = name
        this.life = life * 250;                            //250 - 5000
        this.atkPhysical = atkPhysical * 5;                //0 - 120
        this.criticalChance = criticalChance * 5;          //0 - 100
        this.criticalDamage = 150 + (criticalDamage * 5);  //150 - 200
        this.atkMagic = Convert.ToDouble(atkMagic) * 3.75; //0 - 90
        this.magicPenetration = magicPenetration * 5;      //0 - 120
        this.resistencePhysical = resistencePhysical * 250;//0 - 5000
        this.resistenceMagic = resistenceMagic * 160;      //0 - 4000
        typeDamage = GetTypeDamage(atkPhysical, atkMagic, out this.specialAttack); //Physical, Magic ou Hybrid
    }

    //Maximum physical defense is 5000 //level 3 rarity
    public double DamagePhysicalReceived(double atkEnemy, double resistencePhysicalMy)
    {
        double defenseVariable = Convert.ToDouble(resistencePhysicalMy) * 0.02;

        if(defenseVariable >= 85) 
        {
            defenseVariable = 85;//maximum physical reduced damage in percentage
        }

        double attackReceived = Convert.ToDouble(atkEnemy) * (1 - defenseVariable / 100);sdf
        return attackReceived;
    } 

    //Maximum Magic defense is 4000 // level 1 rarity
    public double DamageMagicReceived(double atkEnemyMagic, double resistenceMagicMy, double magicPenetrationEnermy) {
        double penMagic = Convert.ToDouble(magicPenetrationEnermy) * 0.8333333333333333;
        double defenseVariable = (Convert.ToDouble(resistenceMagicMy) * 0.025) * (1 - penMagic / 100);
        double attackReceived = Convert.ToDouble(atkEnemyMagic) * (1 - defenseVariable / 100);
        return attackReceived;
    }

    //variable of Chance Critical
    public int chanceCriticalVariable() 
    {
        Random random = new Random();
        return random.Next(0, 101);
    }

    //attack physical
    public double AllOutAttackPhysical (double atk, double chanceCritical, double criticalDamage, int CriticalChanceV) 
    {
        if(chanceCritical >= CriticalChanceV)
        {
            return Convert.ToDouble(atk) * (Convert.ToDouble(criticalDamage) / 100); 
        } 
        else 
        {
            return Convert.ToDouble(atk);
        }
    }

    //getting the damage type in string and int
    public string GetTypeDamage(int atkPhy, double atkMag, out int specialAttack)
    {
        if(atkPhy > atkMag && atkMag == 0)
        {   
            specialAttack = 1;
            return "Physical";
        }
        else if(atkPhy < atkMag && atkPhy == 0)
        {
            specialAttack = 2;
            return "Magic";
        }
        else 
        {   
            specialAttack = 3;
            return "Hybrid";
        }
    }

    //multiplier for the special strike
    public double specialAttackReceived(double atkPhyRec, double atkMagRec, int typeDamage) 
    {
        Random random = new Random();
        int GetLuck = random.Next(1,13);
        int luck = 0;

        if(GetLuck > 0 && GetLuck <= 8) 
        {
            luck = 4;
        }
        else if(GetLuck >= 9 && GetLuck <= 11)
        {
            luck = 6;
        }
        else 
        {
            luck = 15;
        }

        if(typeDamage == 1)
        {
            return atkPhyRec * luck;
        }
        else if(typeDamage == 2)
        {
            return atkMagRec * luck;
        }
        else 
        {
            return (atkPhyRec * luck) + (atkMagRec * luck);
        }
    }

    //armor increase + buff + damage based on resistance (based on the type of damage, if you are dealing with physical damage, it will return damage from physical resistance, the same thing with magic and hybrid)
    public double MassiveDefenseReturn(out double resPhy, out double resMag, double resPU, double resMU, int typeDamageReceived)
    {
        resPhy = resPU * 2;
        resMag = resMU * 2;

        if(typeDamageReceived == 1) 
        {
            return (resPhy * (0.02));
        }
        else if(typeDamageReceived == 2)
        {
            return (resMag * (0.03));
        }
        else 
        {
            return (resPhy + resMag) * 0.01;
        }
    }


    //if the skill is available
    public bool NumberOfSkills(int N) {
        if(N >= 3) 
        {
            return true;
        }
        else if(N >= 2) 
        {   
            return false;
        }
        else 
        {
            return false;
        }
    }


    //combat mechanics function
    public int UseSkill(bool canUse1, bool canUse2, string typeD)
    {   do{

            if(canUse1 == true && canUse2 == false)
            {
                Console.WriteLine($"[1] {typeD} basic attack");
                Console.WriteLine($"[2] Use massive defense");
                string getSkill = Console.ReadLine();
                if(!int.TryParse(getSkill, out int getNum)) 
                {
                    Console.WriteLine("put a number");
                    continue;
                }
                else
                {
                    if(getNum != 1 && getNum != 2)
                    {
                        Console.WriteLine("unavailable value");
                        continue;
                    }
                    else
                    {
                        return getNum;
                    }
                }
            }
            else if(canUse1 == true && canUse2 == true) 
            {
                Console.WriteLine($"[1] {typeD} basic attack");
                Console.WriteLine($"[2] Use massive defense");
                Console.WriteLine($"[3] Use {typeD} special attack");
                string getSkill = Console.ReadLine();
                if(!int.TryParse(getSkill, out int getNum)) 
                {
                    Console.WriteLine("put a number");
                    continue;
                }
                else
                {
                    if(getNum != 1 && getNum != 2 && getNum != 3)
                    {
                        Console.WriteLine("unavailable value");
                        continue;
                    }
                    else
                    {
                        return getNum;
                    }
                }
            }
            else if(canUse1 == false && canUse2 == true)
            {
                Console.WriteLine($"[1] {typeD} basic attack");
                Console.WriteLine($"[2] Use {typeD} special attack");
                string getSkill = Console.ReadLine();
                if(!int.TryParse(getSkill, out int getNum)) 
                {
                    Console.WriteLine("put a number");
                    continue;
                }
                else
                {
                    if(getNum != 1 && getNum != 2)
                    {
                        Console.WriteLine("unavailable value");
                        continue;
                    }
                    else
                    {
                        if(getNum == 1) 
                        {
                            return 1;
                        }
                        else 
                        {
                            return 3;
                        }
                    }
                }
            }
            else 
            {
                Console.WriteLine($"[1] {typeD} basic attack");
                string getSkill = Console.ReadLine();
                if(!int.TryParse(getSkill, out int getNum)) 
                {
                    Console.WriteLine("put a number");
                    continue;
                }
                else
                {
                    if(getNum != 1)
                    {
                        Console.WriteLine("unavailable value");
                        continue;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
        }
        while(true);
    }


}


//a little training on inheritance (ignore, but.... if there's something i don't know, let me know)


class Player:PlayerRPG 
{
    public Player(string name, double life, int atkPhysical, int criticalChance, int criticalDamage, double atkMagic, int magicPenetration, int resistencePhysical, int resistenceMagic):base(name, life, atkPhysical, criticalChance, criticalDamage, atkMagic, magicPenetration, resistencePhysical, resistenceMagic)
    {

    }
}

//----------------------------------------------------------------------PROGRAM-----------------------------------------------------------------

class Program 
{
    static void Main() 
    {

        int[] statusPlayer1 = new int[7]; 
        int[] statusPlayer2 = new int[7];
        string[] statusString = new string[8] {"life", "physical attack", "critical chance", "critical damage", "magic damage", "magic penetration", "physical resistence", "magic resistence"};
        int[] minStatus = new int[8] {1, 0, 0, 0, 0, 0, 0, 0};
        int[] maxStatus = new int[8] {20,24,20,10,24,24,20,25};
        string nameP1 = "player1", nameP2 = "player2"; //default name  



        CreatePlayer createPlayer = new CreatePlayer();


        

            //beginning of the game (getting response is attributes)
            do
            {
                Console.WriteLine("Will it be a fight with another player? Yes / No");
                string getAnswer = Convert.ToString(Console.ReadLine());
                if(getAnswer != "Yes" && getAnswer != "yes" && getAnswer != "No" && getAnswer != "no") 
                {
                    Console.WriteLine("value not accepted, restarting process");
                    continue;
                }
                else
                {
                    if(getAnswer == "Yes" || getAnswer == "yes")
                    {




                        Console.WriteLine(
                            "\n------------------------------------------------\n\n"+
                            $"+ Point distribution information\n" +
                            $"+ Life min/max : 250-5000 (1 point - 20 points)\n" + 
                            $"+ Physical attack min/max : 0-120 (0 point - 24 points)\n" +
                            $"+ Critical chance min/max : 0%-100% (0 point - 20 points)\n" + 
                            $"+ critical damage min/max : 150-200 (0 point - 10 points)\n" +
                            $"+ magic attack min/max : 0-90 (0 point - 24 points)\n" +
                            $"+ magic penetration min/max : 0-120 (0 point - 24 points)\n" +
                            $"+ Physical resistance min/max : 0-5000 (0 point - 20 points)\n" + 
                            $"+ Magic resistance min/max : 0-4000 (0 point - 25 points)\n"
                        );
                        Console.WriteLine(
                            "------------------------------------------------\n\n"+
                            "If at the end there are missing points that were not used, they will be assigned first to life, then to your damage (physical/magic), then to your multipliers (critical chance/critical damage/magic penetration)."
                        );

                            //getting the name of the first player
                            Console.WriteLine("\n------------------------------------------------\n\n");
                            Console.WriteLine("Player name 1:");
                            nameP1 = Console.ReadLine();
                            if(nameP1 == "")
                            {
                                nameP1 = "Radiant Knight";
                            }
                            


                        statusPlayer1 = createPlayer.ReturnPlayerStatus(statusString, minStatus, maxStatus, 70, 1);

                        //getting the name of the second player
                            Console.WriteLine("\n------------------------------------------------\n");
                            Console.WriteLine("Player name 2:");
                            nameP2 = Console.ReadLine();
                            if(nameP2 == "") 
                            {
                                nameP2 = "Skeleton Knight";
                            }
                        
                        statusPlayer2 = createPlayer.ReturnPlayerStatus(statusString, minStatus, maxStatus, 70, 2);
                

                        break;
                    }
                    else if(getAnswer == "No" || getAnswer == "no")
                    {
                        Console.WriteLine("the end");
                        return;
                    }

            
                    
                }
                ;
            }
            while(true);
        

            //create player 1
            PlayerRPG player1 = new Player(nameP1, statusPlayer1[0], statusPlayer1[1], statusPlayer1[2], statusPlayer1[3], statusPlayer1[4], statusPlayer1[5], statusPlayer1[6], statusPlayer1[7] );
            //create player 2
            PlayerRPG player2 = new Player(nameP2, statusPlayer2[0], statusPlayer2[1], statusPlayer2[2], statusPlayer2[3], statusPlayer2[4], statusPlayer2[5], statusPlayer2[6], statusPlayer2[7] );


            double defPhyP1 = player1.resistencePhysical;
            double defPhyP2 = player2.resistencePhysical;

            double defMagP1 = player1.resistenceMagic;
            double defMagP2 = player2.resistenceMagic;

            //how many turns to use the move again
            int specialAttackP1 = 3;
            int specialAttackP2 = 3;
            int massiveDefenseP1 = 3;
            int massiveDefenseP2 = 3;



            //start of the fight
            do
            {
                //first player's turn
                if(player1.life >= 1)
                {
                    int skillUsed = 0;

                    Console.WriteLine(
                        "\n-------------------------\n" +
                        $"+ it's {player1.name} turn\n" +
                        "-------------------------\n" );

                    //taking the result of the coup
                    skillUsed = player1.UseSkill(player1.NumberOfSkills(massiveDefenseP1), player1.NumberOfSkills(specialAttackP1), player1.typeDamage);

                        //attack basic
                        if(skillUsed == 1) 
                        {   
                            if(player1.specialAttack == 1) 
                            {
                                double player2DamRec = 0;
                                player2DamRec = player2.DamagePhysicalReceived(player1.AllOutAttackPhysical(player1.atkPhysical, player1.criticalChance, player1.criticalDamage, player1.chanceCriticalVariable()), player2.resistencePhysical);
                                player2.life -= player2DamRec;
                                Console.WriteLine($"The player {player2.name} took {Convert.ToInt32(player2DamRec)} damage and was left with {Convert.ToInt32(player2.life)} health");
                            }
                            else if(player1.specialAttack == 2)
                            {
                                double player2DamRec = 0;
                                player2DamRec = player2.DamageMagicReceived(player1.atkMagic, player2.resistenceMagic, player1.magicPenetration);
                                player2.life -= player2DamRec;
                                Console.WriteLine($"The player {player2.name} took {Convert.ToInt32(player2DamRec)} damage and was left with {Convert.ToInt32(player2.life)} health");
                            }
                            else 
                            {
                                double player2DamRec = 0;
                                player2DamRec = player2.DamagePhysicalReceived(player1.AllOutAttackPhysical(player1.atkPhysical, player1.criticalChance, player1.criticalDamage, player1.chanceCriticalVariable()), player2.resistencePhysical) + player2.DamageMagicReceived(player1.atkMagic, player2.resistenceMagic, player1.magicPenetration);
                                player2.life -= player2DamRec;
                                Console.WriteLine($"The player {player2.name} took {Convert.ToInt32(player2DamRec)} damage and was left with {Convert.ToInt32(player2.life)} health");
                            }
                            if(massiveDefenseP1 == 2) 
                            {
                                massiveDefenseP1 = 1;
                            }
                            else if(massiveDefenseP1 == 1)
                            {
                                massiveDefenseP1 = 3;
                            }

                            if(specialAttackP1 == 2)
                            {
                                specialAttackP1 = 1;
                            }
                            else if(specialAttackP1 == 1)
                            {
                                specialAttackP1 = 3;
                            }

                        }//massive defense
                        else if(skillUsed == 2)
                        {
                            double player2DamRec = 0;
                            player2DamRec = player1.MassiveDefenseReturn(out player1.resistencePhysical, out player1.resistenceMagic, player1.resistencePhysical, player1.resistenceMagic, player1.specialAttack);
                            player2.life -= player2DamRec;
                            Console.WriteLine($"The player {player1.name} doubled his defenses and dealt {Convert.ToInt32(player2DamRec)} damage based on his defense to {player2.name} who had {Convert.ToInt32(player2.life)} life");
                            massiveDefenseP1--;
                            
                            if(specialAttackP1 == 2)
                            {
                                specialAttackP1 = 1;
                            }
                            else if(specialAttackP1 == 1)
                            {
                                specialAttackP1 = 3;
                            }

                        }
                        else //special attack
                        {
                            double player2DamRec2 = 0;
                            player2DamRec2 = player1.specialAttackReceived(player2.DamagePhysicalReceived(player1.AllOutAttackPhysical(player1.atkPhysical, player1.criticalChance, player1.criticalDamage, player1.chanceCriticalVariable()), player2.resistencePhysical), player2.DamageMagicReceived(player1.atkMagic, player2.resistenceMagic, player1.magicPenetration) , player1.specialAttack);
                            player2.life -= player2DamRec2;
                            Console.WriteLine($"The player {player1.name} used is special {player1.typeDamage} damage and dealt {Convert.ToInt32(player2DamRec2)} damage to {player2.name} who had {Convert.ToInt32(player2.life)} health");
                            specialAttackP1--;
                            if(massiveDefenseP1 == 2) 
                            {
                                massiveDefenseP1 = 1;
                            }
                            else if(massiveDefenseP1 == 1)
                            {
                                massiveDefenseP1 = 3;
                            }
                        }

                }
                //player 2 died
                if(player2.life <= 0)
                {
                    Console.WriteLine($"The player {player2.name} lost the match");
                    break;
                }
                //removing armor buff
                player2.resistencePhysical = defPhyP2;
                player2.resistenceMagic = defMagP2;






                //first player's turn
                if(player2.life >= 1)
                {
                    int skillUsed = 0;

                    Console.WriteLine(
                        "\n-------------------------\n" +
                        $"+ it's {player2.name} turn\n" +
                        "-------------------------\n" );

                    //taking the result of the coup
                    skillUsed = player2.UseSkill(player2.NumberOfSkills(massiveDefenseP2), player2.NumberOfSkills(specialAttackP2), player2.typeDamage);

                        //attack basic
                        if(skillUsed == 1) 
                        {   
                            if(player2.specialAttack == 1) 
                            {
                                double player1DamRec = 0;
                                player1DamRec = player1.DamagePhysicalReceived(player2.AllOutAttackPhysical(player2.atkPhysical, player2.criticalChance, player2.criticalDamage, player2.chanceCriticalVariable()), player1.resistencePhysical);
                                player1.life -= player1DamRec;
                                Console.WriteLine($"The player {player1.name} took {Convert.ToInt32(player1DamRec)} damage and was left with {Convert.ToInt32(player1.life)} health");
                            }
                            else if(player2.specialAttack == 2)
                            {
                                double player1DamRec = 0;
                                player1DamRec = player1.DamageMagicReceived(player1.atkMagic, player1.resistenceMagic, player2.magicPenetration);
                                player1.life -= player1DamRec;
                                Console.WriteLine($"The player {player1.name} took {Convert.ToInt32(player1DamRec)} damage and was left with {Convert.ToInt32(player1.life)} health");
                            }
                            else 
                            {
                                double player1DamRec = 0;
                                player1DamRec = player1.DamagePhysicalReceived(player2.AllOutAttackPhysical(player2.atkPhysical, player2.criticalChance, player2.criticalDamage, player2.chanceCriticalVariable()), player1.resistencePhysical) + player1.DamageMagicReceived(player2.atkMagic, player1.resistenceMagic, player2.magicPenetration);
                                player1.life -= player1DamRec;
                                Console.WriteLine($"The player {player1.name} took {Convert.ToInt32(player1DamRec)} damage and was left with {Convert.ToInt32(player1.life)} health");
                            }
                            if(massiveDefenseP2 == 2)
                            {
                                massiveDefenseP2 = 1;
                            }
                            else if(massiveDefenseP2 == 1)
                            {
                                massiveDefenseP2 = 3;
                            }
                            if(specialAttackP2 == 2)
                            {
                                specialAttackP2 = 1;
                            }
                            else if(specialAttackP2 == 1)
                            {
                                specialAttackP2 = 3;
                            }

                        }//massive defense
                        else if(skillUsed == 2)
                        {
                            double player1DamRec = 0;
                            player1DamRec = player2.MassiveDefenseReturn(out player2.resistencePhysical, out player2.resistenceMagic, player2.resistencePhysical, player2.resistenceMagic, player2.specialAttack);
                            player1.life -= player1DamRec;
                            Console.WriteLine($"The player {player2.name} doubled his defenses and dealt {Convert.ToInt32(player1DamRec)} damage based on his defense to {player1.name} who had {Convert.ToInt32(player1.life)} life");
                            massiveDefenseP2--;
                            if(specialAttackP2 == 2)
                            {
                                specialAttackP2 = 1;
                            }
                            else if(specialAttackP2 == 1)
                            {
                                specialAttackP2 = 3;
                            }
                        }//special attack
                        else 
                        {
                            double player1DamRec2 = 0;
                            player1DamRec2 = player2.specialAttackReceived(player1.DamagePhysicalReceived(player2.AllOutAttackPhysical(player2.atkPhysical, player2.criticalChance, player2.criticalDamage, player2.chanceCriticalVariable()), player1.resistencePhysical), player1.DamageMagicReceived(player2.atkMagic, player1.resistenceMagic, player2.magicPenetration) , player2.specialAttack);
                            player1.life -= player1DamRec2;
                            Console.WriteLine($"The player {player2.name} used is special {player2.typeDamage} damage and dealt {Convert.ToInt32(player1DamRec2)} damage to {player1.name} who had {Convert.ToInt32(player1.life)} health");
                            specialAttackP2--;
                            if(massiveDefenseP2 == 2)
                            {
                                massiveDefenseP2 = 1;
                            }
                            else if(massiveDefenseP2 == 1)
                            {
                                massiveDefenseP2 = 3;
                            }
                        }
                }



                //player 1 died
                if(player1.life <= 0) 
                {
                    Console.WriteLine($"The player {player1.name} lost the match");
                    break;
                }

                player1.resistencePhysical = defPhyP1;
                player1.resistenceMagic = defMagP1;
                
            }
            while(true);

    }
    
}