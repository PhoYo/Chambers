namespace Example
{
    // This is an introduction/brief explanation of encapsulation OOP design principle.


    // Let's take an example code that has a board and some tiles on it.
    // We want to damage a tile from somewhere else (some user interaction).


    // Let's start with the simplest version - a static board, nothing fancy at all:
    
    public static class Board1
    {
        public static int[,] tileID;
        public static int[,] tileHealth;
        public static int[,] tileCost;
    }

    public class Input1
    {
        public static void DamageTile(int x, int y)
        {
            Board1.tileHealth[x, y]--;
        }
    }

    // So here the board is static, accessible from everywhere, you can easily change tile health from anywhere.
    // Urgh.
    // You ask "what's so bad?". If there was an easy "oh, gotcha" answer, no one would be making the problem.
    // So, let's first go through the alternatives so we can compare.
    
    // First thing to make this OOP is to get rid of any static`s and
    // make sure everything is class instances (that is, "objects", as in the "object-oriented programming").

    public class Board2
    {
        public int[,] tileID;
        public int[,] tileHealth;
        public int[,] tileCost;
    }

    public class Input2
    {
        public void DamageTile(Board2 board, int x, int y)
        {
            board.tileHealth[x, y]--;
        }
    }

    // The board is now an instance we talk to.
    // (For this example, I will give Input the board to use.)

    // The main issue now is that the board lets anyone change its tiles, so let's prevent that:

    public class Board3
    {
        private int[,] tileID;
        private int[,] tileHealth;
        private int[,] tileCost;

        public void DamageTile(int x, int y)
        {
            tileHealth[x, y]--;
        }
    }

    public class Input3
    {
        public void DamageTile(Board3 board, int x, int y)
        {
            board.DamageTile(x, y);
        }
    }

    // Now we have encapsulated tile damaging functionality within the board class,
    // so no one else can damage the tile but the board. Great.
    
    // Now let's address another OOP principle - single responsibility
    // The board now has 2 responsibilities - manage tile collection and manage individual tiles.

    // So let's make a class for the Tile:

    public class Board4
    {
        public Tile4[,] tile;

        public void DamageTile(int x, int y)
        {
            tile[x, y].health--;
        }
    }

    public class Tile4
    {
        public int ID;
        public int health;
        public int cost;
    }

    public class Input4
    {
        public void DamageTile(Board4 board, int x, int y)
        {
            board.DamageTile(x, y);
        }
    }

    // Awesome, now the tile data is encapsulated!
    // Of course, the board is still damaging it directly, so let's encapsulate tile damaging as well.

    public class Board5
    {
        public Tile5[,] tile;

        public void DamageTile(int x, int y)
        {
            tile[x, y].Damage();
        }
    }

    public class Tile5
    {
        public int ID { get; private set; }
        public int health { get; private set; }
        public int cost { get; private set; }

        public void Damage()
        {
            health--;
        }
    }

    public class Input5
    {
        public void DamageTile(Board5 board, int x, int y)
        {
            board.DamageTile(x, y);
        }
    }

    // Bam, now the tile is fully encapsulated, and so is the board.
    // For now, let us be the final "proper" version.

    // Now you ask "so why is this important? same result with code twice as long."
    // Short answer: because the code will change -- code needs maintenance.
    // Let's, for example, add a damage counter -- how many times a tile has been damaged (easy enough).

    // First let's do this for the first (static board) example

    public static class Board1B
    {
        public static int[,] tileID;
        public static int[,] tileHealth;
        public static int[,] tileTimesDamaged;
        public static int[,] tileCost;
    }

    public class Input1B
    {
        public static void DamageTile(int x, int y)
        {
            Board1B.tileHealth[x, y]--;
            Board1B.tileTimesDamaged[x, y]++;
        }
    }

    // Looks easy enough.

    // And the same for the final proper example:

    public class Board5B
    {
        public Tile5B[,] tile;

        public void DamageTile(int x, int y)
        {
            tile[x, y].Damage();
        }
    }

    public class Tile5B
    {
        public int ID { get; private set; }
        public int health { get; private set; }
        public int timesDamaged { get; private set; }
        public int cost { get; private set; }

        public void Damage()
        {
            health--;
            timesDamaged++;
        }
    }

    public class Input5B
    {
        public void DamageTile(Board5B board, int x, int y)
        {
            board.DamageTile(x, y);
        }
    }

    // Looks just as easy, though there's more code to look through.
    // So the change is just as quick, but the initial code took longer.
    // So your obvious comment is still "but that didn't explain why you made the code longer!? still the same change!"
    // The answer is -- it is not about the changes you DO make, it is about the changes you DIDN'T make.

    // In the first example, did you know that this class introduced a bug?:

    public class AoeDamager1B
    {
        public static void DamageTilesAround(int x, int y)
        {
            Board1B.tileHealth[x, y]--;
            Board1B.tileHealth[x + 1, y + 1]--;
            Board1B.tileHealth[x - 1, y + 1]--;
            Board1B.tileHealth[x + 1, y - 1]--;
            Board1B.tileHealth[x - 1, y - 1]--;
        }
    }

    // The damage counter is never increased here! Uh-oh.

    // You didn't know this, because you never saw this class (even though it's right here, like 30 lines further).
    // After all, why would you notice it unless you go looking for anyone accessing tileHealth.
    // You made an assumption and that is why the proper example still works:

    public class AoeDamager5B
    {
        public static void DamageTilesAround(Board5B board, int x, int y)
        {
            board.DamageTile(x, y);
            board.DamageTile(x + 1, y + 1);
            board.DamageTile(x - 1, y + 1);
            board.DamageTile(x + 1, y - 1);
            board.DamageTile(x - 1, y - 1);
        }
    }
    
    // This version was never given a chance to create a bug.
    // In fact, this version hasn't evene changed since version 3 (which is in itself great).
    // There is only 1 place responsible for increasing damage counter -- 
    // and that is the place where the tile is damaged.
    // There is no other way to damage the tile and so there is no way to bug the damage counter.
}