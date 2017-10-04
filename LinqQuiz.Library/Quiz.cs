using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {
            return (from num in (Enumerable.Range(1, exclusiveUpperLimit - 1)) where (num % 2) == 0 select num).ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            if (exclusiveUpperLimit < 1)
            {
                return new int[0];
            }
            else
            {
                return checked(from num in (Enumerable.Range(1, exclusiveUpperLimit - 1)) where (num % 7) == 0 orderby num descending select (num * num)).ToArray();
                
            }
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {
            int familyId;
            int numberOfFamilyMembers;
            decimal averageAge;

            int sum;

            if (families == null)
                throw new ArgumentNullException();

            List<FamilySummary> summaries = new List<FamilySummary>();

            foreach (IFamily family in families)
            {
                sum = 0;
                numberOfFamilyMembers = 0;
                averageAge = 0;

                foreach (IPerson person in family.Persons)
                    sum += (int)person.Age;

                familyId = family.ID;
                numberOfFamilyMembers = family.Persons.Count;
                if (numberOfFamilyMembers != 0)
                    averageAge = (decimal)sum / numberOfFamilyMembers;

                FamilySummary summary = new FamilySummary();
                summary.FamilyID = familyId;
                summary.NumberOfFamilyMembers = numberOfFamilyMembers;
                summary.AverageAge = averageAge;

                summaries.Add(summary);
            }

            return summaries.ToArray();
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            char firstLetter = 'A';
            char lastlLetter = 'Z';

            int count;

            List<(char letter, int numberOfOccurances)> list = new List<(char letter, int numberOfOccurances)>();

            for (int i = firstLetter; i <= lastlLetter; i++)
            {
                count = 0;

                for (int j = 0; j < text.Length; j++)
                {
                    if (i == (char)text[j])
                        count++;
                }

                if (count != 0)
                    list.Add(((char)i, count));
            }

            return list.ToArray();
        }
    }
}
