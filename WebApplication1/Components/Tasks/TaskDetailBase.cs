using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using WebApplication1.Data.Tasks;
using WebApplication1.ServerCode.DataAccess;


namespace WebApplication1.Components.Tasks {

    public class TaskDetailBase : ComponentBase {
        [ParameterAttribute]
        public string id { get; set; }

        protected TaskDto task {get; set;}
            [Inject]
            protected IGenericDataAccessor dataAccessor {get; set;}
        protected override async Task OnInitAsync()
        {
            task = dataAccessor.find<TaskDto>(Guid.Parse(id));
        }
    } 
}