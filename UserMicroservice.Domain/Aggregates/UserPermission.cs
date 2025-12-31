namespace UserMicroservice.Domain.Entities
{
    public class UserPermission
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        // Inicializa propriedades não anuláveis para evitar CS8618
        protected UserPermission()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public UserPermission(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Nome da permissão é obrigatório.");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        // renomear permissão
        public void Rename(string newName)
        {
            if (string.IsNullOrEmpty(newName))
                throw new ArgumentException("Novo nome da permissão não pode ser vazio.");

            Name = newName;
        }

        // atualizar descrição
        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrEmpty(newDescription))
                throw new ArgumentException("Descrição não pode ser vazia.");

            Description = newDescription;
        }
    }
}