using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ToTicTac
{
    public partial class Form1 : Form
    {
        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            initializer(GameBoard,pGameBoard);
        }
        // Make three differeant 2d arrays: An array for x, An array for O, and an array for clicked buttons. 
        State[][] GameBoard = new State[3][];
        State[][] pGameBoard = new State[3][];
        public enum State
        {
            N,
            X,
            O
        };
        public enum Turns
        {
            X,
            O
        };

        Turns currentTurn = 0;
        int turns = 0;
        private void TicTacToeButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            click(clickedButton);

            System.Threading.Thread.Sleep(200);
            while (currentTurn == Turns.O)
            {
                enemyclick();
            }
        }

        public void enemyclick()
        {
            Button correctb = new Button();
            if (false)
            {
            }
            else if (false)
            {
            }
            else
            {
                correctb = randomclick();
            }
            click(correctb);
        }
        public Button randomclick()
        {
            int ri = r.Next(8);
            foreach (Control control in Controls)
            {
                if (control is Button)
                {
                    Button button = (Button)control;
                    int buttonID = int.Parse(button.Tag.ToString());
                    if (buttonID == ri)
                    {
                        return button;
                    }
                }
            }
            return button0;
        }
        public void click(Button clickedButton)
        {
            //changes the button. increases turn.
            changer(clickedButton, ref currentTurn);
            //checks if won
            if (checker(GameBoard) == State.X)
            {
                MessageBox.Show("X Wins!!!");
                initializer(GameBoard,pGameBoard);
            }
            else if (checker(GameBoard) == State.O)
            {
                MessageBox.Show("O Wins!!!");
                initializer(GameBoard,pGameBoard);
            }
            //changes turn
            turn(ref currentTurn);
        }
        public void turn(ref Turns currentTurn)
        {
            if (turns > 8)
            {
                MessageBox.Show("Tie!!!");
                initializer(GameBoard,pGameBoard);
            }
            if (currentTurn.ToString() == "2")
            {
                label2.Text = "X";
                currentTurn = 0;
            }
        }
        public void changer(Button clickedButton, ref Turns currentTurn)
        {
            int buttonID = int.Parse(clickedButton.Tag.ToString());

            //Zero-based row and column
            int row = buttonID / 3;
            int col = buttonID % 3;
            if (GameBoard[row][col] == State.N)
            {
                if (currentTurn == Turns.X)
                {
                    GameBoard[row][col] = State.X;
                    pGameBoard[row][col] = State.X;
                }
                else
                {
                    GameBoard[row][col] = State.O;
                    pGameBoard[row][col] = State.O;
                }
                clickedButton.Text = GameBoard[row][col].ToString();
                currentTurn++;
                turns++;
            }
            label2.Text = currentTurn.ToString();
        }
        public State checker(State[][] arrray)
        {
            for (int i = 0; i < arrray.Length; i++)
            {
                bool x = checks(arrray[i], State.X);
                bool o = checks(arrray[i], State.O);
                if (!x && !o)
                {
                    State[] bo1 = new State[3];
                    for (int k = 0; k < arrray[i].Length; k++)
                    {
                        bo1[k] = arrray[k][i]; 
                    }
                    x = checks(bo1,State.X);
                    o = checks(bo1, State.O);
                    
                    //diagonal checking
                    if (!x && !o)
                    {
                        State[] bo2 = new State[3];
                        State[] bo3 = new State[3];
                        for (int k = 0; k < arrray[i].Length; k++)
                        {
                            bo2[k] = arrray[k][k];
                            bo3[k] = arrray[k][arrray[i].Length - (k + 1)];
                        }
                        x = checks(bo2, State.X);
                        o = checks(bo2, State.O);
                        if (!x && !o)
                        {
                            x = checks(bo3, State.X);
                            o = checks(bo3, State.O);
                        }
                    }
                }
                if (x)
                {
                    return State.X;
                }
                else if (o)
                {
                    return State.O;
                }
            }

            return State.N;
        }

        public bool checks(State[] arrray, State state)
        {
            return arrray.All<State>(n => n == state);
        }

        public void initializer(State[][] arrray, State[][] arrrray)
        {
            for (int i = 0; i < 3; i++)
            {
                arrray[i] = new State[3];
                arrrray[i] = new State[3];
                for (int k = 0; k < 3; k++)
                {
                    arrray[i][k] = 0;
                    arrrray[i][k] = 0;
                }

                foreach (Control control in Controls)
                {
                    if (control is Button)
                    {
                        Button button = (Button)control;
                        button.Text = "?";
                    }
                }
                label2.Text = "X";
                turns = 0;
                currentTurn = 0;
            }
        }
    }
}
