using System.Collections.Generic;
using System.Text;

namespace .EnvialoSimple.Business.Helpers
{
    public class FiltroModel
    {
        /// <summary>
        /// <param name="MailsListsIds">Obtener solo miembros de las listas seleccionadas</param>
        /// <param name="Count">Número de registros como máximo por página</param>
        /// <param name="AbsolutePage">Página de "count" registros a la que se quiere ir. Si count vale 10 y absolutepage vale 3, entonces empezaremos a ver desde el registro 31</param>
        /// <param name="Status">Tipos de estado por los que se desea filtrar la lista</param>
        /// <param name="Filter">Nombre o parte del nombre por el que se filtran los registros</param>
        /// <param name="OrderBy">Campo por el cual estará ordenado el listado</param>
        /// <param name="Desc">Determina si el orden del listado es ascendente (0) o descendente (1)</param>
        /// </summary>
        public List<int> MailListsIds { get; set; }
        public int? Count { get; set; }
        public int? AbsolutePage { get; set; }
        public StatusValue? Status { get; set; }
        public string Filter { get; set; }
        public OrderByValue? OrderBy { get; set; }
        public bool? Desc { get; set; }

        public FiltroModel(List<int> mailListsIds ,int? count, int? absolutePage, StatusValue? status, string filter, OrderByValue? orderBy, bool? desc)
        {
            MailListsIds = mailListsIds;
            if (count.HasValue)
                Count = count;
            if (absolutePage.HasValue)
                AbsolutePage = absolutePage;
            if (status.HasValue)
                Status = status.Value;
            if (!string.IsNullOrEmpty(filter))
                Filter = filter;
            if (orderBy.HasValue)
                OrderBy = orderBy.Value;
            if (desc.HasValue)
                Desc = desc.Value;
            if (MailListsIds == null)
            {
                MailListsIds = new List<int>();
            }

        }

        public string GetSearchQuery(string exceptFilter = "")
        {
            StringBuilder sb = new StringBuilder();

            if (!exceptFilter.Contains("MailListsIds"))
                foreach (var mailListId in MailListsIds)
                {
                    sb.AppendFormat("MailListsIds[]={0}&", mailListId);
                }

            if (!exceptFilter.Contains("count"))
                if (Count.HasValue)
                    sb.AppendFormat("count={0}&", Count.Value);

            if (!exceptFilter.Contains("absolutepage"))
                if (AbsolutePage.HasValue)
                    sb.AppendFormat("absolutepage={0}&", AbsolutePage.Value);

            if (!exceptFilter.Contains("status"))
                if (Status.HasValue)
                    sb.AppendFormat("status={0}&", Status.Value.ToString());

            if (!exceptFilter.Contains("filter"))
                if (!string.IsNullOrEmpty(Filter))
                    sb.AppendFormat("filter={0}&", Filter);

            if (!exceptFilter.Contains("orderBy"))
                if (OrderBy.HasValue)
                    sb.AppendFormat("orderBy={0}&", OrderBy.Value.ToString());

            if (!exceptFilter.Contains("desc"))
                if (Desc.HasValue)
                {
                    int descValue = Desc.Value ? 1 : 0;
                    sb.AppendFormat("desc={0}", descValue);
                }

            return sb.ToString();
        }
    }

    public enum OrderByValue
    {
        name,
        id,
        activememberscount,
        memberscount
    }

    public enum StatusValue
    {
        Active,
        Inactive,
        Completed
    }

    
}
