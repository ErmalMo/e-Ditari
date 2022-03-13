using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Nxenesit;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    public class NxenesiController : BaseApiController
    {

        [HttpGet]
        public async Task<IActionResult> GetNxenesit()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
        
        [HttpGet("{nxenesiID}")]
        public async Task<IActionResult> GetNxenesi(int nxenesiID)
        {   
            return HandleResult(await Mediator.Send(new Details.Query{NxenesiID = nxenesiID}));
        }

        [HttpPost]
        public async Task<IActionResult> ShtoNxenesin(Nxenesi nxenesi){
            return HandleResult(await Mediator.Send(new Create.Command {Nxenesi = nxenesi}));
        }

        [HttpPut("{nxenesiID}")]
        public async Task<IActionResult> editNxenesi(int nxenesiID, Nxenesi nxenesi){
            nxenesi.NxenesiID = nxenesiID;
            return HandleResult(await Mediator.Send(new Edit.Command{Nxenesi = nxenesi}));
        }

        [HttpDelete("{nxenesiID}")]
        public async Task<IActionResult> DeleteNxenesi(int nxenesiID){
            return HandleResult(await Mediator.Send(new Delete.Command{NxenesiID = nxenesiID}));
        }

    }
}