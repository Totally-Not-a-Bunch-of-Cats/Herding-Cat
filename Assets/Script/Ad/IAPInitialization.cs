using UnityEngine;
using UnityEngine.Purchasing;

public class IAPInitialization : IStoreListener
{
    private IStoreController controller;
    private IExtensionProvider extensions;

    public IAPInitialization()
    {
        //var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        //builder.AddProduct("100_gold_coins", ProductType.Consumable, new IDs
        //{
        //    {"corralling_cats.small_star_bundle", GooglePlay.Name},
        //    {"corralling_cats.medium_star_bundle", GooglePlay.Name},
        //    {"corralling_cats.mega_star_bundle", GooglePlay.Name},
        //    {"corralling_cats.bonus_star_bundle", GooglePlay.Name}
        //});

        //UnityPurchasing.Initialize(this, builder);
    }

    /// <summary>
    /// Called when Unity IAP is ready to make purchases.
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        this.controller = controller;
        this.extensions = extensions;
    }

    /// <summary>
    /// Called when a purchase completes.
    ///
    /// May be called at any time after OnInitialized().
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        return PurchaseProcessingResult.Complete;
    }

    /// <summary>
    /// Called when a purchase fails.
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log("Purchase Failed");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("IAP hard failed");
        throw new System.NotImplementedException();
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }
}
