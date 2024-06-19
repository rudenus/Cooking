using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Text;
using Dal;
using Dal.Entities;
using Dal.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

var tableSpoon = new List<string>()
{
    "Столовая ложка",
    "столовая ложка",
    "Столовых ложек",
    "столовых ложек",
    "Столовые ложки",
    "столовые ложки",
    "ст ложки",
    "ст. ложки",
    "ст л",
    "ст л.",
    "ст. л.",
    "ст. л",
};

var teaSpoon = new List<string>()
{
    "Чайная ложка",
    "чайная ложка",
    "Чайных ложек",
    "чайных ложек",
    "Чайные ложки",
    "чайные ложки",
    "ч ложки",
    "ч. ложки",
    "ч л",
    "ч л.",
    "ч. л.",
    "ч. л",
};

var millilitr = new List<string>()
{
    "Мл.",
    "мл.",
    "мл",
    "Мл",
    "Миллилитры",
    "Миллилитров",
    "Миллилитр",
    "миллилитр",
    "миллилитры",
    "миллилитров",
};

var gramm = new List<string>()
{
    "Г.",
    "Г",
    "г",
    "г.",
    "грамм",
    "Грамм",
    "граммов",
    "Граммов",
    "граммах",
    "Граммах",
};

var kilogramm = new List<string>()
{
    "КГ.",
    "КГ",
    "килограмм",
    "килограммов",
};

var litr = new List<string>()
{
    "Литр",
    "литров",
    "Л.",
};

var thing = new List<string>()
{
    "Шт.",
    "Шт",
    "Штук",
    "Штуки",
    "Штука",
};

var cup = new List<string>()
{
    "Стакан",
    "Стакана",
    "Стк",
    "Стк.",
    "Стаканов",
    "Стаканы",
};

var banka = new List<string>()
{
    "Банка",
    "Банок",
    "Банки",
};

var naVskidky = new List<string>()
{
    "Щепотка",
    "щепоточка",
    "На глаз",
    "По вкусу",
    "На вкус",
    "По наитию",
    "Сколько есть",
    "Как пойдет",
    "на кончике ножа"
};

var teeth = new List<string>()
{
    "Зубчиков",
    "Зубчика",
    "Зубчик",
};

var head = new List<string>()
{
    "Головки",
    "Головок",
    "Головка",
};

var piece = new List<string>()
{
    "Кусок",
    "Куска",
    "Кусков",
    "Кусок",
};

var pukchoc = new List<string>()
{
    "Пучок",
    "Пучка",
    "Пучков",
    "Пучки",
};

Random rand = new Random();


var dbContext = new Context();

await SetReplacement("мандарин", true, "апельсин", true, ReplacementLevel.Low);


async Task SetReplacement(string first, bool firstUse, string second, bool secondUse, ReplacementLevel replacementLevel)
{
    var firstSelect = await dbContext.Products
        .Where(x => EF.Functions.ILike(x.Name, "%" + first + "%"))
        .Select(x => x.ProductId)
        .ToArrayAsync();

    var secondSelect = await dbContext.Products
        .Where(x => EF.Functions.ILike(x.Name, "%" + second + "%"))
        .Select(x => x.ProductId)
        .ToArrayAsync();

    var listToInsert = new List<ReplacementProduct>();

    foreach(var item in firstSelect)
    {
        foreach (var item2 in secondSelect)
        {
            if(item == item2)
            {
                continue;
            }

            var existing = listToInsert.Where(x => x.ReplacingId == item && x.ReplacementId == item2
            || x.ReplacingId == item2 && x.ReplacementId == item).Any();

            if (existing)
            {
                continue;
            }

            listToInsert.Add(new ReplacementProduct()
            {
                ReplacementId = item,
                ReplacingId = item2,
                ReplacementLevel = replacementLevel
            });

            listToInsert.Add(new ReplacementProduct()
            {
                ReplacementId = item2,
                ReplacingId = item,
                ReplacementLevel = replacementLevel
            });
        }
    }


    if (firstUse)
    {
        foreach (var item in firstSelect)
        {
            foreach (var item2 in firstSelect)
            {
                if (item == item2)
                {
                    continue;
                }

                var existing = listToInsert.Where(x => x.ReplacingId == item && x.ReplacementId == item2
            || x.ReplacingId == item2 && x.ReplacementId == item).Any();

                if (existing)
                {
                    continue;
                }

                listToInsert.Add(new ReplacementProduct()
                {
                    ReplacementId = item,
                    ReplacingId = item2,
                    ReplacementLevel = replacementLevel
                });
            }
        }
    }

    if (secondUse)
    {
        foreach (var item in secondSelect)
        {
            foreach (var item2 in secondSelect)
            {
                if (item == item2)
                {
                    continue;
                }

                var existing = listToInsert.Where(x => x.ReplacingId == item && x.ReplacementId == item2
            || x.ReplacingId == item2 && x.ReplacementId == item).Any();

                if (existing)
                {
                    continue;
                }

                listToInsert.Add(new ReplacementProduct()
                {
                    ReplacementId = item,
                    ReplacingId = item2,
                    ReplacementLevel = replacementLevel
                });
            }
        }
    }

    dbContext.ReplacementProducts.AddRange(listToInsert);

    await dbContext.SaveChangesAsync();
}


var config1 = Configuration.Default.WithDefaultLoader();
var context1 = BrowsingContext.New(config1);



//for (int page = 2; true; page++)
//{
//    var allRecipeNames = await dbContext.Recipes
//        .Select(x => x.Name)
//        .Distinct()
//        .ToDictionaryAsync(x => x);

//    var allProductNames = await dbContext.Products
//        .GroupBy(
//        x => x.Name.ToLower(),
//        y => y.ProductId,
//        (key, numbers) => new
//        {
//            Name = key,
//            ProductId = numbers.Max(x => x.ToString())
//        })
//        .ToDictionaryAsync(x => x.Name, y => y.ProductId, StringComparer.OrdinalIgnoreCase);

//    var address1 = $"https://www.russianfood.com/recipes/bytype/?fid=791&page={page}#rcp_list";
//    var document1 = await context1.OpenAsync(address1);
//    var recipesSelector = ".card";
//    var recipes = document1.QuerySelectorAll(recipesSelector);

//    foreach (var recipe in recipes)
//    {
//        var recipeNameSelector = ".title h3";
//        var recipeName = recipe.QuerySelector(recipeNameSelector)?.TextContent;

//        if (string.IsNullOrEmpty(recipeName))
//        {
//            continue;
//        }

//        if (allRecipeNames.ContainsKey(recipeName))
//        {
//            continue;
//        }

//        var productsSelector = ".announce_sub span";
//        var products = recipe.QuerySelector(productsSelector)?.TextContent;

//        if (string.IsNullOrEmpty(products))
//        {
//            continue;
//        }

//        if(products.Length < 10)
//        {
//            continue;
//        }

//        var productListJoined = products[10..];
//        var recipeId = Guid.NewGuid();
//        var productList = productListJoined.Split(", ");
//        var resultIngridientList = new List<Ingridient>(productList.Length);

//        foreach (var product in productList)
//        {
//            if (!allProductNames.ContainsKey(product))
//            {

//                var productId = Guid.NewGuid();
//                resultIngridientList.Add( new Ingridient()
//                {
//                    RecipeId = recipeId,
//                    Product = new Product()
//                    {
//                        IsModerated = false,
//                        Name = product,
//                        ProductId = productId,
//                        IsTest = true
//                    },
//                    IngridientId = Guid.NewGuid()
//                });
//            }

//            else
//            {
//                resultIngridientList.Add(new Ingridient()
//                {
//                    RecipeId = recipeId,
//                    ProductId = new Guid(allProductNames[product]),
//                    IngridientId = Guid.NewGuid(),
//                });
//            }
//        }

//        var recipeResult = new Recipe()
//        {
//            IsModerated = false,
//            RecipeId = recipeId,
//            Name = recipeName,
//            IsTest = true,
//            UserId = new Guid("42edea0a-bb98-4a86-8e3e-a295238df5ed")
//        };

//        dbContext.Add(recipeResult);
//        dbContext.AddRange(resultIngridientList);

//        await dbContext.SaveChangesAsync();
        
//    }
//}




//for (int page = 14; true; page++)
//{
//    var address1 = "https://eda.ru/recepty?page=" + page;
//    var document1 = await context1.OpenAsync(address1);

//    var recipeSelector = ".emotion-18hxz5k";

//    var recipes = document1.QuerySelectorAll(recipeSelector);

//    foreach (var recipe in recipes)
//    {
//        var spanNode = recipe.QuerySelector("SPAN");

//        var recipeRelativeLink = recipe.GetAttribute("href");
//        var recipeName = spanNode?.TextContent;

//        if (string.IsNullOrEmpty(recipeName))
//        {
//            continue;
//        }

//        if (allRecipeNames.ContainsKey(recipeName))
//        {
//            continue;
//        }

//        if (string.IsNullOrEmpty(recipeRelativeLink))
//        {
//            continue;
//        }

//        var recipeAbsolutLink = "https://eda.ru" + recipeRelativeLink;

//        Debug.WriteLine($"Страница - {page}, рецепт - {recipeName}");
//        await RecipeWork(recipeAbsolutLink, recipeName);
//        await Task.Delay(rand.Next(7712, 20653));
//    }
//}



//var allProductNames = dbContext.Products
//    .Select(x => x.Name)
//    .Distinct()
//    .ToDictionary(x => x);

//var config1 = Configuration.Default.WithDefaultLoader();
//var context1 = BrowsingContext.New(config1);
//var address1 = "https://www.sochetaizer.ru/goods/caloricity?page=27";
//var document1 = await context1.OpenAsync(address1);

//var productNodesSelector = ".goods-table-row";
//var productNodes = document1.QuerySelectorAll(productNodesSelector);

//var resultList = new List<Product>(productNodes.Length);

//foreach(var productNode in productNodes)
//{
//    var cellSelector = ".goods-table-cell";
//    var cells = productNode.QuerySelectorAll(cellSelector);
//    if(cells.Length != 5)
//    {
//        continue;
//    }

//    var productNameNode = cells[0].QuerySelector(".goods-table-item-name");

//    if(productNameNode == null || string.IsNullOrEmpty(productNameNode.TextContent))
//    {
//        continue;
//    }

//    //if (allProductNames.ContainsKey(productNameNode.TextContent))
//    //{
//    //    continue;
//    //}

//    var nonDigitsRegex = new Regex(@"[^\d]");

//    int GetInt(string input)
//    {
//        var inputToParse = new string(input.Where(x => x.IsDigit() || x == '.').ToArray());
//        if (string.IsNullOrEmpty(inputToParse))
//        {
//            return 0;
//        }
//        decimal decimalValue = decimal.Parse(inputToParse.Replace('.',','));
//        return (int)Math.Round(decimalValue);
//    }

//    var calories = GetInt(cells[1].TextContent);
//    var proteins = GetInt(cells[2].TextContent);
//    var fats = GetInt(cells[3].TextContent);
//    var carbohydrates = GetInt(cells[4].TextContent);

//    resultList.Add(new Product()
//    {
//        Name = productNameNode.TextContent,
//        Calories = calories,
//        Carbohydrates = carbohydrates,
//        Fats = fats,
//        Proteins = proteins,
//        ProductId = Guid.NewGuid(),
//        IsModerated = true
//    });
//}

//dbContext.AddRange(resultList);
//var productNamesToDelete = resultList.Select(x => x.Name);
//var productsToDelete = dbContext.Products.Where(p => productNamesToDelete.Contains(p.Name)).ToList();
////dbContext.RemoveRange(productsToDelete);
//dbContext.SaveChanges();
//Console.WriteLine("Success");


async Task RecipeWork(string link, string name)
{

    var document1 = await context1.OpenAsync(link);

    Guid recipeId = Guid.NewGuid();

    var recipe = GetBaseRecipe(document1, recipeId);

    if(recipe == null)
    {
        Debug.WriteLine($"Рецепт вернулся пустым - {name}");
        return;
    }

    recipe.Name = name;

    var ingridiens = await GetIngridients(document1, recipeId);

    recipe.Weight = ingridiens.Sum(x => x.Weight);
    if(recipe.Weight <= 0)
    {
        return;
    }

    if (!ingridiens.Any())
    {
        return;
    }

    var operations = GetOperations(document1, recipeId);

    if (!operations.Any())
    {
        return;
    }

    dbContext.Add(recipe);
    dbContext.AddRange(ingridiens);
    dbContext.AddRange(operations);

    await dbContext.SaveChangesAsync();

    Debug.WriteLine($"Рецепт успешно создан {name}");
}

Recipe GetBaseRecipe(IDocument document, Guid recipeId)
{
    var calorisSelector = "span[itemprop=\"calories\"]";
    var caloreis = document.QuerySelector(calorisSelector)?.TextContent;

    if (string.IsNullOrEmpty(caloreis) || !decimal.TryParse(caloreis.Replace('.', ','), out decimal caloreisResult))
    {
        return null;
    }


    var proteinsSelector = "span[itemprop=\"proteinContent\"]";
    var proteins = document.QuerySelector(proteinsSelector)?.TextContent;

    if (string.IsNullOrEmpty(proteins) || !decimal.TryParse(proteins.Replace('.', ','), out decimal proteinsResult))
    {
        return null;
    }

    var fatsSelector = "span[itemprop=\"fatContent\"]";
    var fats = document.QuerySelector(fatsSelector)?.TextContent;

    if (string.IsNullOrEmpty(fats) || !decimal.TryParse(fats.Replace('.', ','), out decimal fatsResult))
    {
        return null;
    }

    var carbohydratesSelector = "span[itemprop=\"carbohydrateContent\"]";
    var carbohydrates = document.QuerySelector(carbohydratesSelector)?.TextContent;

    if (string.IsNullOrEmpty(carbohydrates) || !decimal.TryParse(carbohydrates.Replace('.', ','), out decimal carbohydratesResult))
    {
        return null;
    }

    var descriptionSelector = ".emotion-aiknw3";
    var description = document.QuerySelector(descriptionSelector)?.TextContent;
    

    if (string.IsNullOrEmpty(description))
    {
        return null;
    }

    return new Recipe()
    {
        Calories = (int)Math.Round(caloreisResult),
        Carbohydrates = (int)Math.Round(carbohydratesResult),
        Fats = (int)Math.Round(fatsResult),
        Proteins = (int)Math.Round(proteinsResult),
        RecipeId = recipeId,
        Description = description,
        UserId = new Guid("42edea0a-bb98-4a86-8e3e-a295238df5ed"),
        IsModerated = true
    };

}


async Task<IEnumerable<Ingridient>> GetIngridients(IDocument document, Guid recipeId)
{

    string ingridientSelector = "span[itemprop=\"recipeIngredient\"]";
    var ingridients = document.QuerySelectorAll(ingridientSelector);

    List<Ingridient> ingridientsResult = new List<Ingridient>();

    foreach (var ingridient in ingridients)
    {

        string weightSelector = ".emotion-bsdd3p";
        var weightText = ingridient?.ParentElement?.ParentElement?.ParentElement?.QuerySelector(weightSelector)?.TextContent;
        if (string.IsNullOrEmpty(weightText))
        {
            return Enumerable.Empty<Ingridient>();
        }

        var normalazidWeightText = Regex.Replace(weightText, @"\s", " ").Trim();

        var splitedWeightText = normalazidWeightText.Split(' ');

        decimal? weightSearched = null;
        string? weightTextSearched = null;

        foreach(var weightTextPart in splitedWeightText)
        {
            if(decimal.TryParse(weightTextPart.Replace('.', ','), out decimal weightRelatively))
            {
                weightSearched = weightRelatively;
                weightTextSearched = weightTextPart;
            }
            if(weightTextPart == "½")
            {
                weightSearched = 0.5m;
                weightTextSearched = weightTextPart;
            }
        }


        if (!weightSearched.HasValue)
        {
            if(naVskidky.Any(x => x.Contains(normalazidWeightText, StringComparison.OrdinalIgnoreCase)))
            {
                weightSearched = 10;
            }
            else
            {
                Debug.WriteLine("Не найден вес");
                return Enumerable.Empty<Ingridient>();
            }
        }

        var productName = ingridient.TextContent;

        decimal valueAbsolut = weightSearched.Value;

        var weightTextWithoutDecimalValue = string.Join(' ', splitedWeightText.Where(x => x != weightTextSearched)).Trim();


        var coefficient = 1;

        if (tableSpoon.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 15;
        }

        else if (teaSpoon.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 5;
        }

        else if (millilitr.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 1;
        }

        else if (gramm.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 1;
        }

        else if (thing.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            if(productName.Contains("яйцо", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 50;
            }
            else if (productName.Contains("яблок", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 200;
            }

            else if (productName.Contains("помидор", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 100;
            }

            else if (productName.Contains("помидор", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 100;
            }

            else if (productName.Contains("лук", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 100;
            }

            else if (productName.Contains("карто", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 100;
            }

            else 
            {
                coefficient = 100;
                //return Enumerable.Empty<Ingridient>();
            }
        }

        else if (cup.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 200;
        }

        else if (banka.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 200;
        }

        else if (naVskidky.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 10;
        }

        else if (teeth.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 5;
        }

        else if (head.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 80;
        }

        else if (pukchoc.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 80;
        }

        else if (piece.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            if(productName.Contains("хлеб", StringComparison.OrdinalIgnoreCase))
            {
                coefficient = 50;
            }
            else
            {
                return Enumerable.Empty<Ingridient>();
            }
        }

        else if (kilogramm.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 1000;
        }

        else if (litr.Any(x => x.Contains(weightTextWithoutDecimalValue, StringComparison.OrdinalIgnoreCase)))
        {
            coefficient = 1000;
        }


        else
        {
            Debug.WriteLine($"Не найдена единица измерения{weightTextWithoutDecimalValue}");
            return Enumerable.Empty<Ingridient>();
        }

        valueAbsolut *= coefficient;


        var existedProduct = await TryFindProduct(productName);
        if (!existedProduct.HasValue)
        {
            return Enumerable.Empty<Ingridient>();
        }

        ingridientsResult.Add(new Ingridient()
        {
            IngridientId = Guid.NewGuid(),
            RecipeId = recipeId,
            ProductId = existedProduct.Value,
            Weight = (int)Math.Round(valueAbsolut)
        });
    }

    return ingridientsResult;
}

async Task<Guid?> TryFindProduct(string name)
{
    name = name.Trim();
    Guid? productParName = await dbContext.Products
        .Where(p => EF.Functions.ILike(p.Name, "%" + name + "%"))
        .Select(x => x.ProductId)
        .Cast<Guid?>()
        .FirstOrDefaultAsync();

    if (productParName.HasValue)
    {
        return productParName.Value;
    }

    var splitedName = name.Split(' ');

    if(splitedName.Length == 2)
    {
        var reverseName = splitedName[1] + " " + splitedName[0];

        Guid? newProductParName = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + reverseName + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (newProductParName.HasValue)
        {
            return newProductParName.Value;
        }
    }

    if(splitedName.Length == 3)
    {
        var try1 = splitedName[0] + " " + splitedName[1];
        var try2 = splitedName[0] + " " + splitedName[2];

        var try3 = splitedName[1] + " " + splitedName[0];
        var try4 = splitedName[1] + " " + splitedName[2];

        var try5 = splitedName[2] + " " + splitedName[0];
        var try6 = splitedName[2] + " " + splitedName[1];

        Guid? productTry1 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try1 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry1.HasValue)
        {
            return productTry1.Value;
        }

        Guid? productTry2 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try2 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry2.HasValue)
        {
            return productTry2.Value;
        }

        Guid? productTry3 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try3 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry3.HasValue)
        {
            return productTry3.Value;
        }

        Guid? productTry4 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try4 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry4.HasValue)
        {
            return productTry4.Value;
        }

        Guid? productTry5 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try5 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry5.HasValue)
        {
            return productTry5.Value;
        }

        Guid? productTry6 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try6 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry6.HasValue)
        {
            return productTry6.Value;
        }
    }

    if (splitedName.Length == 4)
    {
        var try1 = splitedName[0] + " " + splitedName[1];
        var try2 = splitedName[0] + " " + splitedName[2];
        var try3 = splitedName[0] + " " + splitedName[3];

        var try4 = splitedName[1] + " " + splitedName[0];
        var try5 = splitedName[1] + " " + splitedName[2];
        var try6 = splitedName[1] + " " + splitedName[3];

        var try7 = splitedName[2] + " " + splitedName[0];
        var try8 = splitedName[2] + " " + splitedName[1];
        var try9 = splitedName[2] + " " + splitedName[3];

        var try10 = splitedName[3] + " " + splitedName[0];
        var try11 = splitedName[3] + " " + splitedName[1];
        var try12 = splitedName[3] + " " + splitedName[2];

        Guid? productTry1 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try1 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry1.HasValue)
        {
            return productTry1.Value;
        }

        Guid? productTry2 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try2 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry2.HasValue)
        {
            return productTry2.Value;
        }

        Guid? productTry3 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try3 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry3.HasValue)
        {
            return productTry3.Value;
        }

        Guid? productTry4 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try4 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry4.HasValue)
        {
            return productTry4.Value;
        }

        Guid? productTry5 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try5 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry5.HasValue)
        {
            return productTry5.Value;
        }

        Guid? productTry6 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try6 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry6.HasValue)
        {
            return productTry6.Value;
        }

        Guid? productTry7 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try7 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry7.HasValue)
        {
            return productTry7.Value;
        }

        Guid? productTry8 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try8 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry8.HasValue)
        {
            return productTry8.Value;
        }

        Guid? productTry9 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try9 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry9.HasValue)
        {
            return productTry9.Value;
        }

        Guid? productTry10 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try10 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry10.HasValue)
        {
            return productTry10.Value;
        }

        Guid? productTry11 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try11 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry11.HasValue)
        {
            return productTry11.Value;
        }

        Guid? productTry12 = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.Name, "%" + try12 + "%"))
            .Select(x => x.ProductId)
            .Cast<Guid?>()
            .FirstOrDefaultAsync();

        if (productTry12.HasValue)
        {
            return productTry12.Value;
        }
    }

    Debug.WriteLine($"Не найден продукт {name}");

    return null;
}


IEnumerable<Operation> GetOperations(IDocument document, Guid recipeId)
{
    var stepSelector = "div[itemprop=\"recipeInstructions\"]";
    var stepNodes = document.QuerySelectorAll(stepSelector);

    var result = new List<Operation>();

    for (int i = 0; i < stepNodes.Length; i++)
    {
        var stepNode = stepNodes[i];

        var imageMetaSelector = "meta[itemprop=\"image\"]";
        var imageMeta = stepNode.QuerySelector(imageMetaSelector);
        if (imageMeta == null)
        {
            break;
        }

        var imageLink = imageMeta.GetAttribute("src");
        if (string.IsNullOrEmpty(imageLink))
        {
            break;
        }
        byte[] imageBytes;
        using (var webClient = new WebClient())
        {
            imageBytes = webClient.DownloadData(imageLink);
        }

        var fileName = imageLink.Split('/').Last();

        var description = stepNode.TextContent;

        if (description[0].IsDigit())
        {
            description = description[1..];
        }

        var fileId = Guid.NewGuid();
        Operation operation = new Operation()
        {
            RecipeId = recipeId,
            Description = description,
            File = new Dal.Entities.File()
            {
                FileId = fileId,
                Name = fileName,
                Content = imageBytes,
                Type = Dal.Enums.FileType.Photo
            },
            FileId = fileId,
            Step = i + 1,
        };

        result.Add(operation);
    }
    return result;
}











//var dbContext = new Context();

//var config1 = Configuration.Default.WithDefaultLoader();
//var address1 = "https://www.calc.ru/Kaloriynost-Produktov-Tablitsa-Kaloriynosti-Produktov.html";
//var context1 = BrowsingContext.New(config1);
//var document1 = await context1.OpenAsync(address1);

//var productSelector = "table[cellpadding=\"2\"] tbody tr";
//var productNodes = document1.QuerySelectorAll(productSelector);

//var products = new List<Product>(productNodes.Length);

//for (int i = 0; i < productNodes.Length; i++)
//{
//    var productNode = productNodes[i];


//    var tdNodes = productNode.Children.Where(x => x.TagName == "TD").ToArray();

//    bool withoutWeight = false;

//    if (tdNodes.Length < 6)
//    {
//        if (tdNodes.Length == 5)
//        {
//            withoutWeight = true;
//        }
//        else
//        {
//            continue;
//        }
//    }

//    Product product = new Product();

//    product.ProductId = Guid.NewGuid();
//    product.IsModerated = true;

//    bool breakInnerLoop = false;

//    for (int j = 0; j < tdNodes.Length && breakInnerLoop == false; j++)
//    {
//        var tdNode = tdNodes[j];

//        var value = tdNode.TextContent;

//        var normalizedValue = Regex.Replace(value, @"\s", " ").Trim().Replace('.', ',');

//        if (!decimal.TryParse(normalizedValue, out decimal decimalValue) && j != 0)
//        {
//            breakInnerLoop = true;
//            continue;
//        }

//        int index = j;
//        if (withoutWeight == true)
//        {
//            index = index + 1;
//        }

//        var valueForSet = (int)Math.Round(decimalValue);

//        if (j == 0)
//        {
//            product.Name = normalizedValue;
//        }

//        else if (index == 2)
//        {
//            product.Proteins = valueForSet;
//        }

//        else if (index == 3)
//        {
//            product.Fats = valueForSet;
//        }

//        else if (index == 4)
//        {
//            product.Carbohydrates = valueForSet;
//        }

//        else if (index == 5)
//        {
//            product.Calories = valueForSet;
//        }
//    }

//    if (!breakInnerLoop)
//    {
//        products.Add(product);
//    }
//}

//dbContext.AddRange(products);

//await dbContext.SaveChangesAsync();