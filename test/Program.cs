using System;
using System.Reflection;
using BloogBot.AI;
using BloogBot.AI.SharedStates;
using BloogBot.Game;
using BloogBot.Game.Objects;
using Newtonsoft.Json;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a method name: ");

        //string fp = "E:\\Fishing.json";
        //var a = File.ReadAllText(fp);
        IBotState CreateMoveToPositionState(Stack<IBotState> botStates, ActionList actionList) => new MoveToPositionState(botStates, actionList);
        //IBotState CreateFaceTo(Stack<IBotState> botStates, ActionList actionList) => new FaceTo(botStates, actionList);
        //IBotState CreateFishingState(Stack<IBotState> botStates, ActionList actionList) => new FishingState(botStates, actionList);

        List<Position> waypoints = new List<Position>();
        List<int> actionIndexList = new List<int>();
        List<int> nextactionIndexList = new List<int>();
        List<Func<Stack<IBotState>, ActionList, IBotState>> stateStack = new List<Func<Stack<IBotState>, ActionList, IBotState>>();

        Console.WriteLine(waypoints.Count);
            waypoints.Add(new Position((float)-8838.341, (float)634.9344, (float)94.64573, (int)0));
            waypoints.Add(new Position((float)-8842.917, (float)641.5321, (float)95.70744, (int)1));
            waypoints.Add(new Position((float)-8846.967, (float)651.1913, (float)96.78641, (int)2));
            waypoints.Add(new Position((float)-8849.237, (float)661.3346, (float)97.32548, (int)3));
            waypoints.Add(new Position((float)-8827.677, (float)678.1038, (float)97.46655, (int)4));
            waypoints.Add(new Position((float)-8841.397, (float)716.8478, (float)97.5912, (int)5));
            waypoints.Add(new Position((float)-8836.563, (float)727.4146, (float)97.6856, (int)6));
            waypoints.Add(new Position((float)-8801.275, (float)745.8544, (float)97.59063, (int)7));
            waypoints.Add(new Position((float)-8792.931, (float)771.1873, (float)96.33835, (int)8));

            actionIndexList.Add(0);
            actionIndexList.Add(1);
            actionIndexList.Add(2);
            actionIndexList.Add(3);
            actionIndexList.Add(4);
            actionIndexList.Add(5);
            actionIndexList.Add(6);
            actionIndexList.Add(7);
            actionIndexList.Add(8);


            nextactionIndexList.Add(1);
            nextactionIndexList.Add(2);
            nextactionIndexList.Add(3);
            nextactionIndexList.Add(4);
            nextactionIndexList.Add(5);
            nextactionIndexList.Add(6);
            nextactionIndexList.Add(7);
            nextactionIndexList.Add(8);
            nextactionIndexList.Add(-1);
            



            
            stateStack.Add(CreateMoveToPositionState); //index 0
            stateStack.Add(CreateMoveToPositionState); //index 1
            stateStack.Add(CreateMoveToPositionState); //index 2
            stateStack.Add(CreateMoveToPositionState); //index 3
            stateStack.Add(CreateMoveToPositionState); //index 4
            stateStack.Add(CreateMoveToPositionState); //index 5
            stateStack.Add(CreateMoveToPositionState); //index 6
            stateStack.Add(CreateMoveToPositionState); //index 7
            stateStack.Add(CreateMoveToPositionState); //index 8



            
            ActionList actionList = new ActionList(waypoints, actionIndexList, nextactionIndexList,stateStack);
            actionList.ActionIndex = 0;


        string fp = "E:\\Fishing.bin";
        Console.WriteLine(fp);
           
            //WriteToBinaryFile<ActionList>(fp, actionList);


        //read json
        //ActionList actionread = JsonConvert.DeserializeObject<ActionList>(File.ReadAllText(fp));
            

            //WriteToJsonFile<List<Func<Stack<IBotState>, ActionList, IBotState>>>(fp, stateStack);

            //List<Position> positions = ReadFromJsonFile<List<Position>>(fp);
            /*
        for (int i = 0; i < 9; i++)
        {
            Console.WriteLine("id:{0},X:{1},Y:{2},Z:{3},nextid:{4}", actionread.Waypoints.FirstOrDefault(u => u.Id == i).Id, actionread.Waypoints.FirstOrDefault(u => u.Id == i).X, actionread.Waypoints.FirstOrDefault(u => u.Id == i).Y, actionread.Waypoints.FirstOrDefault(u => u.Id == i).Z, actionread.NextActionIndexList[i]);
        }
            */

    }

    public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
    {
        TextWriter writer = null;
        try
        {
            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    public static T ReadFromJsonFile<T>(string filePath) where T : new()
    {
        TextReader reader = null;
        try
        {
            reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<T>(fileContents);
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }

    public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            binaryFormatter.Serialize(stream, objectToWrite);
        }
    }
    public static T ReadFromBinaryFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
    static void RunMethod(Func<string, string> method)
    {
        Console.WriteLine(method("John Doe"));
    }

    static string Method1(string name)
    {
        return "method 1 called : " + name;
    }

    static string Method2(string name)
    {
        return "method 2 called : " + name;
    }
}