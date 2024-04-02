using LogGenerator.Models.Domain;
using LogGenerator.Models.ViewModels;
using LogGenerator.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogGenerator.Controllers
{
    public class SubmissionController : Controller
    {
        private readonly ILogRepository submissionRepository;

        public SubmissionController(ILogRepository logRepository)
        {
            this.submissionRepository = logRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddSubmission { };
            return View(model);
        }

            [HttpPost]
        public async Task<IActionResult> Add(AddSubmission addSubmissionRequest)
        {
            var submission = new Submission
            {
                TableAltered = addSubmissionRequest.TableAltered,
                Action = addSubmissionRequest.Action,
                ColumnEdited = addSubmissionRequest.ColumnEdited,
                RowEdited = addSubmissionRequest.RowEdited,
                PreviousEntry = addSubmissionRequest.PreviousEntry,
                LatestEntry = addSubmissionRequest.LatestEntry,
                IPAddress = addSubmissionRequest.IPAddress,
                TimeStamp = addSubmissionRequest.TimeStamp
            };

            await submissionRepository.AddAsync(submission);

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Call the repository
            var submissions = await submissionRepository.GetAllASync();

            return View(submissions);
        }
    }
}
