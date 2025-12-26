
namespace Controller.code.GameState
{

    public interface IInput
    {
        public void SetInput(string input);
        public string GetInput();

    }
    public interface IOutput
    {
        public string PlayerState();
        public string GunState();

        public string Result();
        public int Rounds();

        public string Message();
    }

    public class Input : IInput
    {
        private string input = "";
        void IInput.SetInput(string input)
        {
            this.input = input;
        }

        string IInput.GetInput()
        {
            return input;
        }


    }

    // public class Ouptut : IOutput
    // {
    //     string IOutput.Result()
    //     {

    //     }
    // }


}