//Maps only positive number from list

using MapReduceLinq;

IEnumerable<int> MapPositiveNumbers(int number)
{
    IList<int> positiveNumbers = new List<int>();
    if (number > 0) positiveNumbers.Add(number);
    return positiveNumbers;
}

// Group results together
int GroupNumbers(int value) => value;

//Reduce function that counts the occurence of each number
IEnumerable<KeyValuePair<int, int>> ReduceNumbers(IGrouping<int, int> grouping) =>
    new[] { new KeyValuePair<int, int>(grouping.Key, grouping.Count()) };

// Generate a list of random numbers between -10 and 10
IList<int> sourceData = new List<int>();
var rand = new Random();
for (int i = 0; i < 10000; i++)
{
    sourceData.Add(rand.Next(-10, 10));
}

// Use MapReduce function
var result = sourceData.AsParallel().MapReduce(
    (Func<int, IEnumerable<int>>)MapPositiveNumbers,
    (Func<int, int>)GroupNumbers,
    (Func<IGrouping<int, int>, IEnumerable<KeyValuePair<int, int>>>)ReduceNumbers);
// process the results
foreach (var item in result)
{
    Console.WriteLine($"{item.Key} found {item.Value} times");
}
