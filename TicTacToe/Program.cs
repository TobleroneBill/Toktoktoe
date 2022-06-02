//GameLoop initialiser
Game game = new Game();
game.Start();

class Game {
    //fields
    private BoardPiece[] _boardArray = new BoardPiece[9];
    private GameLogic logic;
    private bool isplaying;
    private char winner;
    //getters
    public BoardPiece[] GetBoardArray{get => _boardArray;}
    public GameLogic Logic{get => logic;}


    public void Start() {

        while (isplaying) {
            GameLoop();
        }
        Console.WriteLine(UpdateBoard());
        if (winner != 't')
        {
            Console.WriteLine($"Well Done {winner} You beat that other biatch!");
        }
        else {
            Console.WriteLine("You guys tied :/ SMH wth lololol");
        }
    }

    //holds all game steps
    private void GameLoop(){
        Console.Clear();                                //Clear Screen, then update board. check if there someone won & set winner (char) to a winning char(x,o,t) if someone one, or a tie.
        Console.WriteLine(UpdateBoard());
        winner = Logic.CheckBoardPieces(_boardArray);
        CheckWinner();
        if (isplaying)      //if is playing, get input
        {
            Playerin();
        }
        else {              //otherwise recheck winner for no fuckign reason and then clear sceen lmao
            CheckWinner();
            Console.Clear();
        }
    }

    void CheckWinner() {
        if (winner == 'x')
        {
            isplaying = false;
        }
        else if (winner == 'o')
        { isplaying = false; }
        else if (winner == 't') {
            isplaying = false;
        }
    }

    void Playerin()
    {
        int input;      //boardPosition, given by user
        char playerChar;//see below
        if (Logic.GetPlayer) { playerChar = 'x'; }      //set board char to x if p1, o if p2
        else { playerChar = 'o'; }

        Console.WriteLine($"{playerChar} please input location (1-9)");
                            
        while (true) {      //this is a bad solution. Doing it because im lazy :/
            input = Convert.ToInt32(Console.ReadLine()) - 1;    // minus 1 so it works with array properly.
            if (input < 0 || input > 8)                         //numbers are between 0-8 for array purposes, but player doesnt know.
            {                                                   
                Console.WriteLine("Bad input");                 //if not in usable range, tell player thier option was shit then restarts cuz of if
            }
            else
            {
                if (_boardArray[input].GetChar == ' ')          //if the char is a space 'empty', then player can put thier option there.
                {
                    _boardArray[input].GetChar = playerChar;
                    Logic.GetPlayer = !Logic.GetPlayer;         //flip flops who is playing
                    break;                                      //the only way to leave the while loop
                }
                else
                {
                    Console.WriteLine("Thats already been taken");  //restarts and tells player to be ORIGINAL for once ;/
                }
            }
        }
    }


    public String UpdateBoard() {       //update board
        return $"{_boardArray[0].GetChar}|{_boardArray[1].GetChar}|{_boardArray[2].GetChar}" +
                "\n-+-+-" +
                $"\n{_boardArray[3].GetChar}|{_boardArray[4].GetChar}|{_boardArray[5].GetChar}" +
                "\n-+-+-" +
                $"\n{_boardArray[6].GetChar}|{_boardArray[7].GetChar}|{_boardArray[8].GetChar}";

        }

    //constuctor
    public Game() {
        for (int i = 0; i < 9; i++)
        {
            _boardArray[i] = new BoardPiece(i);
        }
        logic = new GameLogic(true);
        isplaying = true;
    }
}

class BoardPiece {
    private int _location;
    private char _char = ' ';

    public char GetChar { get => _char; set => _char = value; }
    public int Location { get => _location; set => _location = value; }   //location needed for logic checks


    public BoardPiece() {
        _char = ' ';
    }

    public BoardPiece(int location) {
        _char = ' ';
        _location = location;
    }
}

class GameLogic {
    bool _player1;
    int tieValue = 36;
    public bool GetPlayer { get => _player1; set => _player1 = value; }

    public char CheckBoardPieces(BoardPiece[] boardPieces) {        //true = p1 false = p2;
        Console.WriteLine(tieValue);
        for (int i = 0; i < boardPieces.Length; i++) {
            if (boardPieces[i].GetChar == 'x') {
                tieValue -= 1;  //-1 for each char that == x or o. if board full, 36 = 0, and starts a tie.

                //switch case checking if a winning move has been found. x cant go at same time as o, so it doesnt matter the order of checking for the symbol (x or o)
                switch (boardPieces[i].Location) {          
                    case 0:
                        if (boardPieces[1].GetChar == 'x' && boardPieces[2].GetChar == 'x')
                        {
                            return 'x';
                        }
                        if (boardPieces[4].GetChar == 'x' && boardPieces[8].GetChar == 'x')
                        {
                            return 'x';
                        }
                        if (boardPieces[3].GetChar == 'x' && boardPieces[6].GetChar == 'x')
                        {
                            return 'x';
                        }
                        break;
                    case 1:
                        if (boardPieces[4].GetChar == 'x' && boardPieces[7].GetChar == 'x')
                        {
                            return 'x';
                        }
                        break;
                    case 2:
                        if (boardPieces[4].GetChar == 'x' && boardPieces[6].GetChar == 'x')
                        {
                            return 'x';
                        }
                        if (boardPieces[5].GetChar == 'x' && boardPieces[8].GetChar == 'x')
                        {
                            return 'x';
                        }
                        break;
                    case 3:
                        if (boardPieces[4].GetChar == 'x' && boardPieces[5].GetChar == 'x')
                        {
                            return 'x';
                        }
                        break;
                }
            }
            else if (boardPieces[i].GetChar == 'o')
            {
                tieValue -= 1;
                switch (boardPieces[i].Location)
                {
                    case 0:
                        if (boardPieces[1].GetChar == 'o' && boardPieces[2].GetChar == 'o')
                        {
                            return 'o'; 
                        }
                        if (boardPieces[4].GetChar == 'o' && boardPieces[8].GetChar == 'o')
                        {
                            return 'o';
                        }
                        if (boardPieces[3].GetChar == 'o' && boardPieces[6].GetChar == 'o')
                        {
                            return 'o';
                        }
                        break;
                    case 1:
                        if (boardPieces[4].GetChar == 'o' && boardPieces[7].GetChar == 'o')
                        {
                            return 'o';
                        }
                        break;
                    case 2:
                        if (boardPieces[4].GetChar == 'o' && boardPieces[6].GetChar == 'o')
                        {
                            return 'o';
                        }
                        if (boardPieces[5].GetChar == 'o' && boardPieces[8].GetChar == 'o')
                        {
                            return 'o';
                        }
                        break;
                    case 3:
                        if (boardPieces[4].GetChar == 'o' && boardPieces[5].GetChar == 'o')
                        {
                            return 'o';
                        }
                        break;
                }
            }
        }

        if (tieValue == 0) {
            return 't';
        }

        return ' ';
    }

    public GameLogic(bool StartingPlayer)
    {
        _player1 = StartingPlayer;
    }
}