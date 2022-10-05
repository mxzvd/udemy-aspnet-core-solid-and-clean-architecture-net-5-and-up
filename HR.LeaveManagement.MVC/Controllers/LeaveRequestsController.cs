using AutoMapper;
using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HR.LeaveManagement.MVC.Controllers;

[Authorize]
public class LeaveRequestsController : Controller
{
    private readonly ILeaveTypeService leaveTypeService;
    private readonly ILeaveRequestService leaveRequestService;
    private readonly IMapper mapper;

    public LeaveRequestsController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService, IMapper mapper)
    {
        this.leaveTypeService = leaveTypeService;
        this.leaveRequestService = leaveRequestService;
        this.mapper = mapper;
    }

    public async Task<ActionResult> Create()
    {
        var leaveTypes = await leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
        var model = new CreateLeaveRequestVM
        {
            LeaveTypes = leaveTypeItems
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
    {
        if (ModelState.IsValid)
        {
            var response = await leaveRequestService.CreateLeaveRequest(leaveRequest);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }

        var leaveTypes = await leaveTypeService.GetLeaveTypes();
        var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
        leaveRequest.LeaveTypes = leaveTypeItems;

        return View(leaveRequest);
    }

    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Index()
    {
        var model = await leaveRequestService.GetAdminLevelRequestList();
        return View(model);
    }

    public async Task<ActionResult> Details(int id)
    {
        var model = await leaveRequestService.GetLeaveRequest(id);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> ApproveRequest(int id, bool approved)
    {
        try
        {
            await leaveRequestService.ApproveLeaveRequest(id, approved);
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
