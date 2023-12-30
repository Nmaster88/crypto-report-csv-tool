using CsvApp.Binance.Application.Dtos;
using CsvApp.Binance.Application.Interfaces;
using System.Text;

namespace CsvApp.Binance.Application.Services
{
    public class CsvAppProcess : ICsvAppProcess
    {
        public List<IncomeGainsDto> Process(List<IncomeGainsDto> list)
        {
            var result = list
                .GroupBy(i => i.Asset)
                .Select(v => new
                {
                    Asset = v.Key,
                    TotalValueEUR = v.Sum(t => t.Value)
                }
                );
            throw new NotImplementedException();
        }

        public List<TransactionsDto> Process(List<TransactionsDto> list)
        {
            throw new NotImplementedException();
        }

        //<AnexoJq092AT01-Linha numero = "2">
        //<NLinha> 952 </NLinha>
        //<CodPais> 276 </CodPais>
        //<Codigo> G01 </Codigo>
        //<AnoRealizacao> 2022 </AnoRealizacao>
        //<MesRealizacao> 6 </MesRealizacao>
        //<ValorRealizacao> 16.60 </ValorRealizacao>
        //<AnoAquisicao> 2021 </AnoAquisicao>
        //<MesAquisicao> 12 </MesAquisicao>
        //<ValorAquisicao> 18.30 </ValorAquisicao>
        //<DespesasEncargos> 0.00 </DespesasEncargos>
        //<ImpostoPagoNoEstrangeiro> 0.00 </ImpostoPagoNoEstrangeiro>
        //<CodPaisContraparte> 528 </CodPaisContraparte>
        //</AnexoJq092AT01 - Linha>
        public List<string> Process(List<RealizedCapitalGainsDto> list)
        {

            list = list.Where(x => x.HoldingPeriodDays < 365).ToList();

            List<string> xmlValues = new List<string>();

            int columnNumber = 2;
            int lineNumber = 952;
            int codPais = 276;

            foreach (var elem in list)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"<AnexoJq092AT01-Linha numero = \"{columnNumber++}\">");
                sb.AppendLine($"<NLinha>\"{lineNumber++}\"</NLinha>");
                sb.AppendLine($"<CodPais>\"{codPais++}\"</CodPais>");
                sb.AppendLine($"<Codigo>\"G01\"</Codigo>");
                sb.AppendLine($"<AnoRealizacao>\"2022\"</AnoRealizacao>");
                sb.AppendLine($"<MesRealizacao>\"6\"</MesRealizacao>");
                sb.AppendLine($"<ValorRealizacao>\"{elem.GainsEur}\"</ValorRealizacao>");
                sb.AppendLine($"<AnoAquisicao>\"2021\"</AnoAquisicao>");
                sb.AppendLine($"<MesAquisicao>\"12\"</MesAquisicao>");
                sb.AppendLine($"<ValorAquisicao>\"{elem.CostBasisEur}\"</ValorAquisicao>");
                sb.AppendLine($"<DespesasEncargos>\"12\"</DespesasEncargos>");
                sb.AppendLine($"<ImpostoPagoNoEstrangeiro>\"12\"</ImpostoPagoNoEstrangeiro>");
                sb.AppendLine($"<CodPaisContraparte>\"528\"</CodPaisContraparte>");
                sb.AppendLine($"</AnexoJq092AT01 - Linha>");

                xmlValues.Add(sb.ToString());
            }

            return xmlValues;
        }
    }
}
