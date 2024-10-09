namespace SnackTech.Common.Dto.Api
{
    public class PagamentoDto
    {
        public string action {get; set;} = default!;
        public string application_id {get; set;} = default!;
        public PagamentoDataDto data {get; set;} = default!;
        public string date_created {get; set;} = default!;
        public string id {get; set;} = default!;
        public bool live_mode {get; set;}
        public string status {get; set;} = default!;
        public string type {get; set;} = default!;
        public long user_id {get; set;} = default!;
        public int version {get; set;}
    }

    public class PagamentoDataDto{
        public string currency_id {get; set;} = default!;
        public string marketplace {get; set;} = default!;
        public string status {get; set;} = default!;
    }
}