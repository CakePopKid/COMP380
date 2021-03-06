﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Features.Deliverables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProjectManagementSystem.Features.Deliverables
{
    public class DeliverableService
    {
        private ApplicationDbContext db;

        public DeliverableService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<List<DeliverableListModel>> GetDeliverableListDataAsync()
        {
            var deliverableDataList = await 
                (
                    from deliverable in this.db.Deliverable
                    //NEED TO LINK REQUIREMENTS ; TASKS
                    select new DeliverableListModel 
                    { 
                        Id = deliverable.Id, 
                        Name = deliverable.Name, 
                        Description = deliverable.Description, 
                        DueDate = deliverable.DueDate 
                    }
                ).ToListAsync();

            return deliverableDataList;
        }

        public async Task<Deliverable> GetDeliverableById(Guid Id) //using Id from DelivModel
        {
            var deliverableModel = await (
                from deliverable in this.db.Deliverable
                where deliverable.Id == Id
                select deliverable
                    ).FirstOrDefaultAsync();

            return deliverableModel;
        }

        public async Task<List<Deliverable>> GetDeliverablesDataAsync()
        {
            var deliverables = await (
                from deliverable in this.db.Deliverable
                select deliverable).ToListAsync();

            return deliverables;
        }

        public int SaveDeliverable(DeliverableModel deliverableData)
        {
            int entriesSaved = 0;

            //taskData is a list of tasks.
            //each has a DeliverableId as FK
            //Have to put deliverableData.Id INTO DeliverableId for the task

            try
            {
                this.db.Add(deliverableData.Deliverable);
                entriesSaved = this.db.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.Write(ex.Message);
            }

            return entriesSaved;
        }
    }
}
