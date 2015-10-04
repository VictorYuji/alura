using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Loja.Models;
using Loja.DAO;


namespace LojaAPI.Controllers
{
    public class CarrinhoController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new CarrinhoDAO().Busca(id));   
            }
            catch(KeyNotFoundException) 
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, new HttpError(string.Format("O carrinho {0} não foi encontrado", id)));
            }
        }

        public HttpResponseMessage Post([FromBody]Carrinho carrinho)
        {
            new CarrinhoDAO().Adiciona(carrinho);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
            string location = Url.Link("DefaultApi", new {controller = "carrinho", id = carrinho.Id});
            response.Headers.Location = new Uri(location);
            return response;

        }
        [Route ("api/carrinho/{idCarrinho}/produto/{idProduto}")]
        public HttpResponseMessage Delete([FromUri]int idCarrinho,[FromUri] int idProduto)
        {
            new CarrinhoDAO().Busca(idCarrinho).Remove(idProduto);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
