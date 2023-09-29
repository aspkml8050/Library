namespace Model.Shared
{
    public class ReturnData<T>
    {
        public bool isSuccess { get; set; }
        public string Mesg { get; set; }
        public T Data { get; set; }
    }

}
