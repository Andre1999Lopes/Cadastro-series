using System;

namespace cadastro_series
{
	public class Serie : BaseEntity
	{
		private Genre genre { get; set; }
		private string title { get; set; }
		private string description { get; set; }
		private int year { get; set; }
		private bool isDeleted { get; set; }

		public Serie(int id, Genre genre, string title, string description, int year)
		{
			this.id = id;
			this.genre = genre;
			this.title = title;
			this.description = description;
			this.year = year;
			isDeleted = false;
		}

		public override string ToString()
		{
			string retorno = "";
			retorno += $"Gênero: {genre}{Environment.NewLine}";
			retorno += $"Título: {title}{Environment.NewLine}";
			retorno += $"Descrição: {description}{Environment.NewLine}";
			retorno += $"Ano de lançamento: {year}{Environment.NewLine}";
			retorno += $"Excluída: {(isDeleted ? "Sim" : "Não")}";
			return retorno;
		}

		public string ReturnTitle()
		{
			return title;
		}

		public Genre ReturnGenre()
		{
			return genre;
		}

		public int ReturnYear()
		{
			return year;
		}

		public string ReturnDescription()
		{
			return description;
		}

		public int ReturnId()
		{
			return id;
		}

		public void Delete()
		{
			isDeleted = true;
		}

		public bool IsDeleted()
		{
			return isDeleted;
		}
	}
}