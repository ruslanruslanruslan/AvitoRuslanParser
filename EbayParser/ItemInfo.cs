namespace AvitoRuslanParser.EbayParser
{

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  [System.Xml.Serialization.XmlRoot(Namespace = "urn:ebay:apis:eBLBaseComponents", IsNullable = false)]
  public partial class GetMultipleItemsResponse
  {

    private string timestampField;

    private string ackField;

    private string buildField;

    private uint versionField;

    private GetMultipleItemsResponseItem[] itemField;

    private GetMultipleItemsResponseErrors errorsField;

    /// <remarks/>
    public string Timestamp
    {
      get
      {
        return timestampField;
      }
      set
      {
        timestampField = value;
      }
    }

    /// <remarks/>
    public string Ack
    {
      get
      {
        return ackField;
      }
      set
      {
        ackField = value;
      }
    }

    /// <remarks/>
    public string Build
    {
      get
      {
        return buildField;
      }
      set
      {
        buildField = value;
      }
    }

    /// <remarks/>
    public uint Version
    {
      get
      {
        return versionField;
      }
      set
      {
        versionField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("Item")]
    public GetMultipleItemsResponseItem[] Item
    {
      get
      {
        return itemField;
      }
      set
      {
        itemField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("Errors")]
    public GetMultipleItemsResponseErrors Errors
    {
      get
      {
        return errorsField;
      }
      set
      {
        errorsField = value;
      }
    }

  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseErrors
  {
    private string shortMessageField;

    private string longMessageField;

    private string errorCodeField;

    private string severityCodeField;

    private GetMultipleItemsResponseErrorsErrorParameters errorParametersField;

    private string errorClassificationField;

    public string ShortMessage
    {
      get
      {
        return shortMessageField;
      }
      set
      {
        shortMessageField = value;
      }
    }

    public string LongMessage
    {
      get
      {
        return longMessageField;
      }
      set
      {
        longMessageField = value;
      }
    }

    public string ErrorCode
    {
      get
      {
        return errorCodeField;
      }
      set
      {
        errorCodeField = value;
      }
    }

    public string SeverityCode
    {
      get
      {
        return severityCodeField;
      }
      set
      {
        severityCodeField = value;
      }
    }

    [System.Xml.Serialization.XmlElement("ErrorParameters")]
    public GetMultipleItemsResponseErrorsErrorParameters ErrorParameters
    {
      get
      {
        return errorParametersField;
      }
      set
      {
        errorParametersField = value;
      }
    }

    public string ErrorClassification
    {
      get
      {
        return errorClassificationField;
      }
      set
      {
        errorClassificationField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseErrorsErrorParameters
  {
    private ulong paramIDField;

    private ulong valueField;

    public ulong ParamID
    {
      get
      {
        return paramIDField;
      }
      set
      {
        paramIDField = value;
      }
    }

    public ulong Value
    {
      get
      {
        return valueField;
      }
      set
      {
        valueField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItem
  {
    [System.Xml.Serialization.XmlElement("Description")]
    public string Description { get; set; }

    [System.Xml.Serialization.XmlElement("BuyItNowAvailable")]
    public bool? BuyItNowAvailable { get; set; }

    private bool bestOfferEnabledField;

    private ulong itemIDField;

    private string endTimeField;

    private string startTimeField;

    private string viewItemURLForNaturalSearchField;

    private string listingTypeField;

    private string locationField;

    private string paymentMethodsField;

    private string galleryURLField;

    private string[] pictureURLField;

    private string postalCodeField;

    private bool postalCodeFieldSpecified;

    private string primaryCategoryNameField;

    private int quantityField;

    private GetMultipleItemsResponseItemSeller sellerField;

    private int bidCountField;

    private GetMultipleItemsResponseItemConvertedCurrentPrice convertedCurrentPriceField;

    private GetMultipleItemsResponseItemCurrentPrice currentPriceField;

    private GetMultipleItemsResponseItemHighBidder highBidderField;

    private string listingStatusField;

    private int quantitySoldField;

    private string[] shipToLocationsField;

    private string siteField;

    private string timeLeftField;

    private string titleField;

    private uint hitCountField;

    private string primaryCategoryIDPathField;

    private GetMultipleItemsResponseItemStorefront storefrontField;

    private string countryField;

    private GetMultipleItemsResponseItemReturnPolicy returnPolicyField;

    private GetMultipleItemsResponseItemMinimumToBid minimumToBidField;

    private GetMultipleItemsResponseItemProductID productIDField;

    private bool autoPayField;

    private bool integratedMerchantCreditCardEnabledField;

    private int handlingTimeField;

    private bool topRatedListingField;

    private bool topRatedListingFieldSpecified;

    private bool globalShippingField;

    private int quantitySoldByPickupInStoreField;

    private bool newBestOfferField;

    /// <remarks/>
    public bool BestOfferEnabled
    {
      get
      {
        return bestOfferEnabledField;
      }
      set
      {
        bestOfferEnabledField = value;
      }
    }

    /// <remarks/>
    public ulong ItemID
    {
      get
      {
        return itemIDField;
      }
      set
      {
        itemIDField = value;
      }
    }

    /// <remarks/>
    public string EndTime
    {
      get
      {
        return endTimeField;
      }
      set
      {
        endTimeField = value;
      }
    }

    /// <remarks/>
    public string StartTime
    {
      get
      {
        return startTimeField;
      }
      set
      {
        startTimeField = value;
      }
    }

    /// <remarks/>
    public string ViewItemURLForNaturalSearch
    {
      get
      {
        return viewItemURLForNaturalSearchField;
      }
      set
      {
        viewItemURLForNaturalSearchField = value;
      }
    }

    /// <remarks/>
    public string ListingType
    {
      get
      {
        return listingTypeField;
      }
      set
      {
        listingTypeField = value;
      }
    }

    /// <remarks/>
    public string Location
    {
      get
      {
        return locationField;
      }
      set
      {
        locationField = value;
      }
    }

    /// <remarks/>
    public string PaymentMethods
    {
      get
      {
        return paymentMethodsField;
      }
      set
      {
        paymentMethodsField = value;
      }
    }

    /// <remarks/>
    public string GalleryURL
    {
      get
      {
        return galleryURLField;
      }
      set
      {
        galleryURLField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("PictureURL")]
    public string[] PictureURL
    {
      get
      {
        return pictureURLField;
      }
      set
      {
        pictureURLField = value;
      }
    }

    /// <remarks/>
    public string PostalCode
    {
      get
      {
        return postalCodeField;
      }
      set
      {
        postalCodeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnore()]
    public bool PostalCodeSpecified
    {
      get
      {
        return postalCodeFieldSpecified;
      }
      set
      {
        postalCodeFieldSpecified = value;
      }
    }

    /// <remarks/>
    public string PrimaryCategoryName
    {
      get
      {
        return primaryCategoryNameField;
      }
      set
      {
        primaryCategoryNameField = value;
      }
    }

    /// <remarks/>
    public int Quantity
    {
      get
      {
        return quantityField;
      }
      set
      {
        quantityField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemSeller Seller
    {
      get
      {
        return sellerField;
      }
      set
      {
        sellerField = value;
      }
    }

    /// <remarks/>
    public int BidCount
    {
      get
      {
        return bidCountField;
      }
      set
      {
        bidCountField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemConvertedCurrentPrice ConvertedCurrentPrice
    {
      get
      {
        return convertedCurrentPriceField;
      }
      set
      {
        convertedCurrentPriceField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemCurrentPrice CurrentPrice
    {
      get
      {
        return currentPriceField;
      }
      set
      {
        currentPriceField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemHighBidder HighBidder
    {
      get
      {
        return highBidderField;
      }
      set
      {
        highBidderField = value;
      }
    }

    /// <remarks/>
    public string ListingStatus
    {
      get
      {
        return listingStatusField;
      }
      set
      {
        listingStatusField = value;
      }
    }

    /// <remarks/>
    public int QuantitySold
    {
      get
      {
        return quantitySoldField;
      }
      set
      {
        this.quantitySoldField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement("ShipToLocations")]
    public string[] ShipToLocations
    {
      get
      {
        return shipToLocationsField;
      }
      set
      {
        shipToLocationsField = value;
      }
    }

    /// <remarks/>
    public string Site
    {
      get
      {
        return siteField;
      }
      set
      {
        siteField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElement(DataType = "duration")]
    public string TimeLeft
    {
      get
      {
        return timeLeftField;
      }
      set
      {
        timeLeftField = value;
      }
    }

    /// <remarks/>
    public string Title
    {
      get
      {
        return titleField;
      }
      set
      {
        titleField = value;
      }
    }

    /// <remarks/>
    public uint HitCount
    {
      get
      {
        return hitCountField;
      }
      set
      {
        hitCountField = value;
      }
    }

    /// <remarks/>
    public string PrimaryCategoryIDPath
    {
      get
      {
        return primaryCategoryIDPathField;
      }
      set
      {
        primaryCategoryIDPathField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemStorefront Storefront
    {
      get
      {
        return storefrontField;
      }
      set
      {
        storefrontField = value;
      }
    }

    /// <remarks/>
    public string Country
    {
      get
      {
        return countryField;
      }
      set
      {
        countryField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemReturnPolicy ReturnPolicy
    {
      get
      {
        return returnPolicyField;
      }
      set
      {
        returnPolicyField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemMinimumToBid MinimumToBid
    {
      get
      {
        return minimumToBidField;
      }
      set
      {
        minimumToBidField = value;
      }
    }

    /// <remarks/>
    public GetMultipleItemsResponseItemProductID ProductID
    {
      get
      {
        return productIDField;
      }
      set
      {
        productIDField = value;
      }
    }

    /// <remarks/>
    public bool AutoPay
    {
      get
      {
        return autoPayField;
      }
      set
      {
        autoPayField = value;
      }
    }

    /// <remarks/>
    public bool IntegratedMerchantCreditCardEnabled
    {
      get
      {
        return integratedMerchantCreditCardEnabledField;
      }
      set
      {
        integratedMerchantCreditCardEnabledField = value;
      }
    }

    /// <remarks/>
    public int HandlingTime
    {
      get
      {
        return handlingTimeField;
      }
      set
      {
        handlingTimeField = value;
      }
    }

    /// <remarks/>
    public bool TopRatedListing
    {
      get
      {
        return topRatedListingField;
      }
      set
      {
        topRatedListingField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnore()]
    public bool TopRatedListingSpecified
    {
      get
      {
        return topRatedListingFieldSpecified;
      }
      set
      {
        topRatedListingFieldSpecified = value;
      }
    }

    /// <remarks/>
    public bool GlobalShipping
    {
      get
      {
        return globalShippingField;
      }
      set
      {
        globalShippingField = value;
      }
    }

    /// <remarks/>
    public int QuantitySoldByPickupInStore
    {
      get
      {
        return quantitySoldByPickupInStoreField;
      }
      set
      {
        quantitySoldByPickupInStoreField = value;
      }
    }

    /// <remarks/>
    public bool NewBestOffer
    {
      get
      {
        return newBestOfferField;
      }
      set
      {
        newBestOfferField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemSeller
  {

    private string userIDField;

    private string feedbackRatingStarField;

    private int feedbackScoreField;

    private decimal positiveFeedbackPercentField;

    private bool topRatedSellerField;

    private bool topRatedSellerFieldSpecified;

    /// <remarks/>
    public string UserID
    {
      get
      {
        return userIDField;
      }
      set
      {
        userIDField = value;
      }
    }

    /// <remarks/>
    public string FeedbackRatingStar
    {
      get
      {
        return feedbackRatingStarField;
      }
      set
      {
        feedbackRatingStarField = value;
      }
    }

    /// <remarks/>
    public int FeedbackScore
    {
      get
      {
        return feedbackScoreField;
      }
      set
      {
        feedbackScoreField = value;
      }
    }

    /// <remarks/>
    public decimal PositiveFeedbackPercent
    {
      get
      {
        return positiveFeedbackPercentField;
      }
      set
      {
        positiveFeedbackPercentField = value;
      }
    }

    /// <remarks/>
    public bool TopRatedSeller
    {
      get
      {
        return topRatedSellerField;
      }
      set
      {
        topRatedSellerField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnore()]
    public bool TopRatedSellerSpecified
    {
      get
      {
        return topRatedSellerFieldSpecified;
      }
      set
      {
        topRatedSellerFieldSpecified = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemConvertedCurrentPrice
  {

    private string currencyIDField;

    private decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string currencyID
    {
      get
      {
        return currencyIDField;
      }
      set
      {
        currencyIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlText()]
    public decimal Value
    {
      get
      {
        return valueField;
      }
      set
      {
        valueField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemCurrentPrice
  {

    private string currencyIDField;

    private decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttribute()]
    public string currencyID
    {
      get
      {
        return currencyIDField;
      }
      set
      {
        currencyIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlText()]
    public decimal Value
    {
      get
      {
        return this.valueField;
      }
      set
      {
        this.valueField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemHighBidder
  {

    private string userIDField;

    private bool feedbackPrivateField;

    private string feedbackRatingStarField;

    private int feedbackScoreField;

    /// <remarks/>
    public string UserID
    {
      get
      {
        return this.userIDField;
      }
      set
      {
        this.userIDField = value;
      }
    }

    /// <remarks/>
    public bool FeedbackPrivate
    {
      get
      {
        return this.feedbackPrivateField;
      }
      set
      {
        this.feedbackPrivateField = value;
      }
    }

    /// <remarks/>
    public string FeedbackRatingStar
    {
      get
      {
        return this.feedbackRatingStarField;
      }
      set
      {
        this.feedbackRatingStarField = value;
      }
    }

    /// <remarks/>
    public int FeedbackScore
    {
      get
      {
        return this.feedbackScoreField;
      }
      set
      {
        this.feedbackScoreField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemStorefront
  {

    private string storeURLField;

    private string storeNameField;

    /// <remarks/>
    public string StoreURL
    {
      get
      {
        return storeURLField;
      }
      set
      {
        storeURLField = value;
      }
    }

    /// <remarks/>
    public string StoreName
    {
      get
      {
        return storeNameField;
      }
      set
      {
        storeNameField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemReturnPolicy
  {

    private string refundField;

    private string returnsWithinField;

    private string returnsAcceptedField;

    private string shippingCostPaidByField;

    /// <remarks/>
    public string Refund
    {
      get
      {
        return refundField;
      }
      set
      {
        refundField = value;
      }
    }

    /// <remarks/>
    public string ReturnsWithin
    {
      get
      {
        return returnsWithinField;
      }
      set
      {
        returnsWithinField = value;
      }
    }

    /// <remarks/>
    public string ReturnsAccepted
    {
      get
      {
        return returnsAcceptedField;
      }
      set
      {
        returnsAcceptedField = value;
      }
    }

    /// <remarks/>
    public string ShippingCostPaidBy
    {
      get
      {
        return shippingCostPaidByField;
      }
      set
      {
        shippingCostPaidByField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemMinimumToBid
  {

    private string currencyIDField;

    private decimal valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string currencyID
    {
      get
      {
        return currencyIDField;
      }
      set
      {
        currencyIDField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public decimal Value
    {
      get
      {
        return valueField;
      }
      set
      {
        valueField = value;
      }
    }
  }

  /// <remarks/>
  [System.Serializable()]
  [System.ComponentModel.DesignerCategory("code")]
  [System.Xml.Serialization.XmlType(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
  public partial class GetMultipleItemsResponseItemProductID
  {

    private string typeField;

    private uint valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type
    {
      get
      {
        return typeField;
      }
      set
      {
        typeField = value;
      }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public uint Value
    {
      get
      {
        return valueField;
      }
      set
      {
        valueField = value;
      }
    }
  }


}
