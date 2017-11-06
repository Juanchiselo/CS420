using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueens
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            int chosenAlgorithm = 0;
            int n = 0;
            int steps = 0;
            int[] board;
            int numberOfInstances = 0;
            int previousHeuristic;
            int puzzlesSolved = 0;
            List<KeyValuePair<int[], int>> puzzles = new List<KeyValuePair<int[], int>>();

            Console.Write("Please enter the number of queens: ");
            n = Convert.ToInt32(Console.ReadLine());

            Console.Write("Please enter the number of instances to run: ");
            numberOfInstances = Convert.ToInt32(Console.ReadLine());

            Console.Write("Which algorithm would you like to run: "
                + "\n\t1) Steepest-Ascent Hill Climb Algorithm"
                + "\n\t2) Genetic Algorithm"
                + "\nChoice: ");
            chosenAlgorithm = Convert.ToInt32(Console.ReadLine());
            
            switch (chosenAlgorithm)
            {
                case 1:
                    // Populate instances.
                    for (int i = 0; i < numberOfInstances; i++)
                    {
                        board = GenerateBoardState(n);
                        while (puzzles.Any(x => x.Key == board && x.Value == CalculateHeuristic(board)))
                            board = GenerateBoardState(n);

                        puzzles.Add(new KeyValuePair<int[], int>(board, CalculateHeuristic(board)));
                    }

                    // Solves the puzzles.
                    foreach (KeyValuePair<int[], int> item in puzzles)
                    {
                        steps = 0;
                        previousHeuristic = int.MaxValue;
                        board = item.Key;

                        //Console.WriteLine("Original Board:\nBoard: [{0}] - Heuristic: {1}", string.Join(",", item.Key), item.Value);

                        while (CalculateHeuristic(board) < previousHeuristic)
                        {
                            board = SteepestAscentHillClimb(item.Key);
                            //Console.WriteLine("Step#{2}\nBoard: [{0}] - Heuristic: {1}",
                            //    string.Join(",", board), CalculateHeuristic(board), steps + 1);
                            steps++;
                            previousHeuristic = CalculateHeuristic(board);
                        }

                        if (CalculateHeuristic(board) == 0)
                            puzzlesSolved++;

                        //Console.WriteLine("Steps to solve: " + steps);
                    }

                    Console.WriteLine("Total Puzzles: {0}\nPuzzles Solved: {1}\nPuzzles Unsolved: {2}",
                        numberOfInstances, puzzlesSolved, (numberOfInstances - puzzlesSolved));
                    break;
                case 2:
                    // GENETIC ALGORITHM
                    List<int[]> population = new List<int[]>(numberOfInstances);

                    // Generate population.
                    for (int i = 0; i < numberOfInstances; i++)
                        population.Add(GenerateBoardState(n));

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < population.Count; j++)
                        {
                            Console.WriteLine("Board: [{0}]", string.Join(",", population.ElementAt(j)));
                        }

                        Genetic(population);
                    }
                    break;
                default:
                    break;
            }
            
            Console.ReadLine();
        }

        private static int[] GenerateBoardState(int n)
        {
            int[] board = new int[n];

            for (int column = 0; column < board.Length; column++)
                board[column] = random.Next(board.Length);

            return board;
        }

        private static int CalculateHeuristic(int[] board)
        {
            int heuristic = 0;

            for(int column = 0; column < board.Length - 1; column++)
            {
                for(int nextColumn = column + 1; nextColumn < board.Length; nextColumn++)
                {
                    // If the rows are the same increment the heuristic.
                    if (board[column] == board[nextColumn])
                        heuristic++;

                    // No need to check columns because only one queen is being place on each column.

                    // If the diagonals are the same increment the heuristic.
                    int diagonalOffset = nextColumn - column;

                    if (board[column] == board[nextColumn] - diagonalOffset
                        || board[column] == board[nextColumn] + diagonalOffset)
                        heuristic++;
                }
            }

            return heuristic;
        }

        private static int[] SteepestAscentHillClimb(int[] board)
        {
            List<KeyValuePair<int[], int>> successors = new List<KeyValuePair<int[], int>>();
            int currentHeuristic = CalculateHeuristic(board);
            int successorHeuristic;
            int bestHeuristic = currentHeuristic;
            

            // Calculate all heuristics for each successor.
            for(int column = 0; column < board.Length; column++)
            {
                for(int row = 0; row < board.Length; row++)
                {                    
                    if(board[column] != row)
                    {
                        int[] successorBoard = (int[])board.Clone();
                        successorBoard[column] = row;

                        successorHeuristic = CalculateHeuristic(successorBoard);

                        var successor = 
                            new KeyValuePair<int[], int>(successorBoard, successorHeuristic);
                        successors.Add(successor);

                        if (successorHeuristic < bestHeuristic)
                            bestHeuristic = successorHeuristic;
                    }
                }                
            }

            List<KeyValuePair<int[], int>> bestSuccessors = new List<KeyValuePair<int[], int>>();

            // Gets the best successors.
            foreach (KeyValuePair<int[], int> successor in successors)
            {
                if (successor.Value == bestHeuristic)
                    bestSuccessors.Add(successor);
            }

            // Chooses a successor from the best successors randomly.
            if(bestSuccessors.Count > 0)
            {
                board = bestSuccessors.ElementAt(random.Next(bestSuccessors.Count)).Key;
            }

            return board;

            //int counter = 1;
            //foreach(KeyValuePair<int[], int> successor in successors)
            //{
            //    Console.WriteLine("#{0} - Board: [{1}] - Heuristic: {2}", counter++, string.Join(",", successor.Key), successor.Value);
            //}

            //counter = 1;
            //Console.WriteLine("Best successors!");
            //foreach (KeyValuePair<int[], int> successor in bestSuccessors)
            //{
            //    Console.WriteLine("#{0} - Board: [{1}] - Heuristic: {2}", counter++, string.Join(",", successor.Key), successor.Value);
            //}
            
        }

        /**STILL NEEDS TO IMPLEMENT THRESHOLD TO IMPROVE POPULATION**/
        private static void Genetic(List<int[]> population)
        {
            /*
             * Population: A set of k randomly generated states.
             * Individual: A state that is represented as a string over a finite alphabet.
             * Fitness Function: The objective function that rates each state.
             *      The fitness function should return higher values for better states.
             *      For the 8-Queens problem the number of nonattacking pairs of queens is used,
             *      which is 28 for a solution.
             */

            int[] individual;
            int[] parentA;
            int[] parentB;
            int threshold = 20;
            int crossoverPoint;
            int populationSize = population.Count;
            int individualLength = population.ElementAt(0).Length;
            List<KeyValuePair<int[], int>> rankedPopulation = new List<KeyValuePair<int[], int>>(population.Count);
            List<KeyValuePair<int[], int>> offspring = new List<KeyValuePair<int[], int>>(population.Count);
            KeyValuePair<int[], int[]> siblings;


            // Gets the fitness values for each individual in the population.
            for (int i = 0; i < population.Count; i++)
            {
                individual = population.ElementAt(i);

                rankedPopulation.Add(new KeyValuePair<int[], int>(individual, CalculateFitness(individual)));                
            }

            // Sorts the population from highest to lowest fitness.
            rankedPopulation.Sort(CompareByFitness);

            Console.WriteLine("Ranked population:");
            PrintPopulation(rankedPopulation);

            // Selects the individuals.
            for(int i = 0; i < populationSize / 2; i++)
            {
                // Prevents having the parent mate with itself.
                do
                {
                    parentA = rankedPopulation.ElementAt(random.Next(populationSize)).Key;
                    parentB = rankedPopulation.ElementAt(random.Next(populationSize)).Key;
                } while (parentA.SequenceEqual(parentB));


                // Prevents having a child be identical to a parent.
                do
                {
                    crossoverPoint = random.Next(individualLength);
                } while (crossoverPoint == 0);                

                // Does the crossover.
                siblings = Crossover(parentA, parentB, crossoverPoint);

                offspring.Add(new KeyValuePair<int[], int>(siblings.Key, CalculateFitness(siblings.Key)));
                offspring.Add(new KeyValuePair<int[], int>(siblings.Value, CalculateFitness(siblings.Value)));
            }
           
            // Does the mutation of the new individual.
            for(int i = 0; i < offspring.Count; i++)
            {
                int mutatedGene = random.Next(individualLength);

                Console.WriteLine("\nBefore Mutation: [{0}]",
                   string.Join(",", offspring.ElementAt(i).Key),
                   mutatedGene);

                offspring.ElementAt(i).Key[mutatedGene]
                    = random.Next(individualLength);

                Console.WriteLine("\n After Mutation: [{0}] Mutated Gene: {1}",
                   string.Join(",", offspring.ElementAt(i).Key),
                   mutatedGene + 1);

            }


            Console.WriteLine("Population size: " + population.Count + "\nOffspring Size: " + offspring.Count);

            // Replace the population with the offspring.
            for(int i = 0; i < offspring.Count; i++)
            {
                population[i] = offspring.ElementAt(i).Key;
            }
        }

        private static KeyValuePair<int[], int[]> Crossover(int[] parentA, int[] parentB, int crossoverPoint)
        {
            int[] offspringA = new int[parentA.Length];
            int[] offspringB = new int[parentA.Length];

            for (int i = 0; i < parentA.Length; i++)
            {
                if (i < crossoverPoint)
                {
                    offspringA[i] = parentA[i];
                    offspringB[i] = parentB[i];
                }
                else
                {
                    offspringA[i] = parentB[i];
                    offspringB[i] = parentA[i];
                }
            }

            Console.WriteLine("\nCrossover Point: {3}\nParentA: [{0}]\nParentB: [{1}]\nOffspring:[{2}]",
                   string.Join(",", parentA),
                   string.Join(",", parentB),
                   string.Join(",", offspringA),
                   crossoverPoint);

            Console.WriteLine("\nCrossover Point: {3}\nParentA: [{0}]\nParentB: [{1}]\nOffspring:[{2}]",
                   string.Join(",", parentA),
                   string.Join(",", parentB),
                   string.Join(",", offspringB),
                   crossoverPoint);


            return new KeyValuePair<int[], int[]>(offspringA, offspringB);
        }

        /// <summary>
        /// The fitness function for the genetic algorithm.
        /// It calculates the fitness of an individual based
        /// on the number of nonattacking pairs of queens.
        /// </summary>
        /// <param name="individual"></param>
        /// <returns></returns>
        private static int CalculateFitness(int[] individual)
        {
            int maximumFitness = 0;
            
            for(int i = 0; i < individual.Length; i++)
                maximumFitness += i;

            return maximumFitness - CalculateHeuristic(individual);
        }

        private static int CompareByFitness(KeyValuePair<int[], int> a,
            KeyValuePair<int[], int> b)
        {
            return -1 * a.Value.CompareTo(b.Value);
        }

        private static void PrintPopulation(List<KeyValuePair<int[], int>> population)
        {
            for(int i = 0; i < population.Count; i++)
            {
                KeyValuePair<int[], int> individual = population.ElementAt(i);
                Console.WriteLine("Board: [{0}] - Fitness: {1}",
                        string.Join(",", individual.Key), individual.Value);
            }
        }
    }
}
