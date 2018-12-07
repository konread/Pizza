using System.Collections.Generic;
using System.Linq;

namespace Model.Helper
{
    public static class HelperIngredient
    {
        public static List<Ingredient> Connect(List<Ingredient> ingredientsPizza, List<Ingredient> ingredientsAll)
        {
            foreach (Ingredient ingredient in ingredientsPizza)
            {
                ingredientsAll.First(item => item.Id_Ingredient == ingredient.Id_Ingredient).Status = true;
            }

            return ingredientsAll;
        }
    }
}