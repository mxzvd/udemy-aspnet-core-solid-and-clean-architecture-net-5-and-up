using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.MVC.Controllers;

[Authorize(Roles = "Administrator")]
public class LeaveTypesController : Controller
{
    private readonly ILeaveTypeService leaveTypeService;
    private readonly ILeaveAllocationService leaveAllocationService;
    
    public LeaveTypesController(ILeaveTypeService leaveTypeService, ILeaveAllocationService leaveAllocationService)
    {
        this.leaveTypeService = leaveTypeService;
        this.leaveAllocationService = leaveAllocationService;
    }

    public async Task<ActionResult> Index()
    {
        var model = await leaveTypeService.GetLeaveTypes();
        return View(model);
    }

    public async Task<ActionResult> Details(int id)
    {
        var model = await leaveTypeService.GetLeaveTypeDetails(id);
        return View(model);
    }

    public async Task<ActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateLeaveTypeVM leaveType)
    {
        try
        {
            var response = await leaveTypeService.CreateLeaveType(leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }
        return View(leaveType);
    }

    public async Task<ActionResult> Edit(int id)
    {
        var model = await leaveTypeService.GetLeaveTypeDetails(id);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, LeaveTypeVM leaveType)
    {
        try
        {
            var response = await leaveTypeService.UpdateLeaveType(id, leaveType);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }
        return View(leaveType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var response = await leaveTypeService.DeleteLeaveType(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", response.ValidationErrors);
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Allocate(int id)
    {
        try
        {
            var response = await leaveAllocationService.CreateLeaveAllocations(id);
            if (response.Success)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        catch (Exception e)
        {
            ModelState.AddModelError("", e.Message);
        }
        return BadRequest();
    }
}
