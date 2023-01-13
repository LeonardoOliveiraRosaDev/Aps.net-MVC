namespace ProjetoMVCAspNet.Models
{
    // Elemento de dados para serem manipulados CRUD

    // Ela estara static, pois sua caracteristica
    // 1º Característica -> classe static so pode ser composta com elementos staticos
    // 2º Caracterítica -> pode ser usada por todo o nosso projeto de maneira globais
    // 3º Característica -> não pode gerar um objeto ( não instancia um objeto da classe)

    public static class Repository
    {
        // todos os elementos da classe estatica devem ser estaticos
        // Criar uma lista utilizando a Colection List<> Model do main
        // objeto do tipo List
        private static List<Employee> allEmployees = new List<Employee>();
        // fazer uso da interce IEnumerable
        // Este processo sera para receber todos os dados de nossa lista
        // podendo ter 1 ou mais dados dessa lista

        // Um elemento que enumera cada um dos registro da lista
        // elemento publico que atribui valor a prop privada
        public static IEnumerable<Employee> AllEmployees 
        { 
            get { return allEmployees; }
        }

        //Definir um método static para gravar/criar registros
        public static void Create(Employee employee)
        {
            // o método add, ele adiciona cada um dos registros de funcionario
            // dentro da lista.
            // que foi criada com a nomenclaturar allEmployees
            // aqui está sendo usando o objeto criado acima do tipo List

            allEmployees.Add(employee);
        }
    }
}
