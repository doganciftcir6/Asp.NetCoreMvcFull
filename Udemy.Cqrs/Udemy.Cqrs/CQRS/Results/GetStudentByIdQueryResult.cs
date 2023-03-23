namespace Udemy.Cqrs.CQRS.Results
{
    //sorgu yapıcam query sayesinde bir ıd getirecek ama sonuç ne burada onu yapıyoruz.
    public class GetStudentByIdQueryResult
    {
        //sonuç olarak gelecek datada bu bilgiler olacak 
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
    }
}
