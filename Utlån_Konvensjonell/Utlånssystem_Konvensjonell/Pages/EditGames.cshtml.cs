using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using Utlånssystem_Konvensjonell.Infrastructure.Data;
using Utlånssystem_Konvensjonell.SharedKernel;

using Utlånssystem_Konvensjonell.Core.Domain.BoardGames;

using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Events;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Handlers;
using Utlånssystem_Konvensjonell.Core.Domain.BoardGames.Services;

using Microsoft.AspNetCore.Authorization;





namespace Utlånssystem_Konvensjonell.Pages;


[Authorize(Roles = "Admin")]
public class EditGameModel : PageModel
{
private readonly BoardGameContext _db;
private readonly AddGameHandler _AddGameHandler;
private readonly RegisteredGameService _RegisteredGameService;
private readonly IWebHostEnvironment _environment;

public EditGameModel(
        BoardGameContext db,
        AddGameHandler addGameHandler,
        RegisteredGameService registeredGameService,
        IWebHostEnvironment environment)
    {
        _db = db;
        _AddGameHandler = addGameHandler;
        _RegisteredGameService = registeredGameService;
        _environment = environment;
    }



[BindProperty]
    public InputGameModel Input { get; set; } = new();

    public class InputGameModel
    {
        [Required]
        public string GameTitle { get; set; } = "";

        [Required]
        public int Quantity { get; set; } = 1;

        [Required]
        public bool Loanable { get; set; } = true;

        public IFormFile? Image { get; set; }


        [Required]
        public string GameDescription { get; set; } = "";

    }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Console.WriteLine($"WebRootPath: {_environment.WebRootPath}");


          var ImagePath = "images/Default.jpg";
          var Image = Input.Image;
    if (Image != null && Image.Length > 0)
    {
        if (Path.GetExtension(Image.FileName) != ".png" && Path.GetExtension(Image.FileName) != ".jpeg" && Path.GetExtension(Image.FileName) != ".jpg")
            {
                return Page();
            }
        var fileName = Input.GameTitle + Path.GetExtension(Image.FileName);
        var imagesFolder = Path.Combine(_environment.WebRootPath, "images");

        // Ensure the images directory exists
        if (!Directory.Exists(imagesFolder))
        {
            Directory.CreateDirectory(imagesFolder);
        }

        var fullPath = Path.Combine(imagesFolder, fileName);

        using (var stream = System.IO.File.Create(fullPath))
        {
            await Image.CopyToAsync(stream);
        }

        ImagePath = "images/" + fileName;
        }
        //var result = await _mediator.Send(new Create.Request(item.Name, item.Description, item.Price, item.CookTime, ImagePath));
        //if (result.Success) return RedirectToPage("Index");

        //Item = item;

        //Errors = result.Errors;
        //return Page();



        _RegisteredGameService.Registered += _AddGameHandler.OnRegistered;
        await _RegisteredGameService.RegisterAsync(Input.GameTitle, Input.Quantity, Input.Loanable, ImagePath, Input.GameDescription);

        return RedirectToPage("/Index");


}}


