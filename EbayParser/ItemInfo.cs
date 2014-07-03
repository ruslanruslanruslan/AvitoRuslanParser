using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvitoRuslanParser.EbayParser
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:ebay:apis:eBLBaseComponents", IsNullable = false)]
    public partial class GetMultipleItemsResponse
    {

        private string timestampField;

        private string ackField;

        private string buildField;

        private uint versionField;

        private GetMultipleItemsResponseItem[] itemField;

        /// <remarks/>
        public string Timestamp
        {
            get
            {
                return this.timestampField;
            }
            set
            {
                this.timestampField = value;
            }
        }

        /// <remarks/>
        public string Ack
        {
            get
            {
                return this.ackField;
            }
            set
            {
                this.ackField = value;
            }
        }

        /// <remarks/>
        public string Build
        {
            get
            {
                return this.buildField;
            }
            set
            {
                this.buildField = value;
            }
        }

        /// <remarks/>
        public uint Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Item")]
        public GetMultipleItemsResponseItem[] Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public partial class GetMultipleItemsResponseItem
    {
        [System.Xml.Serialization.XmlElementAttribute("Description")]
        public string Description { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("BuyItNowAvailable")]
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
                return this.bestOfferEnabledField;
            }
            set
            {
                this.bestOfferEnabledField = value;
            }
        }

        /// <remarks/>
        public ulong ItemID
        {
            get
            {
                return this.itemIDField;
            }
            set
            {
                this.itemIDField = value;
            }
        }

        /// <remarks/>
        public string EndTime
        {
            get
            {
                return this.endTimeField;
            }
            set
            {
                this.endTimeField = value;
            }
        }

        /// <remarks/>
        public string StartTime
        {
            get
            {
                return this.startTimeField;
            }
            set
            {
                this.startTimeField = value;
            }
        }

        /// <remarks/>
        public string ViewItemURLForNaturalSearch
        {
            get
            {
                return this.viewItemURLForNaturalSearchField;
            }
            set
            {
                this.viewItemURLForNaturalSearchField = value;
            }
        }

        /// <remarks/>
        public string ListingType
        {
            get
            {
                return this.listingTypeField;
            }
            set
            {
                this.listingTypeField = value;
            }
        }

        /// <remarks/>
        public string Location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        public string PaymentMethods
        {
            get
            {
                return this.paymentMethodsField;
            }
            set
            {
                this.paymentMethodsField = value;
            }
        }

        /// <remarks/>
        public string GalleryURL
        {
            get
            {
                return this.galleryURLField;
            }
            set
            {
                this.galleryURLField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PictureURL")]
        public string[] PictureURL
        {
            get
            {
                return this.pictureURLField;
            }
            set
            {
                this.pictureURLField = value;
            }
        }

        /// <remarks/>
        public string PostalCode
        {
            get
            {
                return this.postalCodeField;
            }
            set
            {
                this.postalCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PostalCodeSpecified
        {
            get
            {
                return this.postalCodeFieldSpecified;
            }
            set
            {
                this.postalCodeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string PrimaryCategoryName
        {
            get
            {
                return this.primaryCategoryNameField;
            }
            set
            {
                this.primaryCategoryNameField = value;
            }
        }

        /// <remarks/>
        public int Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemSeller Seller
        {
            get
            {
                return this.sellerField;
            }
            set
            {
                this.sellerField = value;
            }
        }

        /// <remarks/>
        public int BidCount
        {
            get
            {
                return this.bidCountField;
            }
            set
            {
                this.bidCountField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemConvertedCurrentPrice ConvertedCurrentPrice
        {
            get
            {
                return this.convertedCurrentPriceField;
            }
            set
            {
                this.convertedCurrentPriceField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemCurrentPrice CurrentPrice
        {
            get
            {
                return this.currentPriceField;
            }
            set
            {
                this.currentPriceField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemHighBidder HighBidder
        {
            get
            {
                return this.highBidderField;
            }
            set
            {
                this.highBidderField = value;
            }
        }

        /// <remarks/>
        public string ListingStatus
        {
            get
            {
                return this.listingStatusField;
            }
            set
            {
                this.listingStatusField = value;
            }
        }

        /// <remarks/>
        public int QuantitySold
        {
            get
            {
                return this.quantitySoldField;
            }
            set
            {
                this.quantitySoldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ShipToLocations")]
        public string[] ShipToLocations
        {
            get
            {
                return this.shipToLocationsField;
            }
            set
            {
                this.shipToLocationsField = value;
            }
        }

        /// <remarks/>
        public string Site
        {
            get
            {
                return this.siteField;
            }
            set
            {
                this.siteField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
        public string TimeLeft
        {
            get
            {
                return this.timeLeftField;
            }
            set
            {
                this.timeLeftField = value;
            }
        }

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public uint HitCount
        {
            get
            {
                return this.hitCountField;
            }
            set
            {
                this.hitCountField = value;
            }
        }

        /// <remarks/>
        public string PrimaryCategoryIDPath
        {
            get
            {
                return this.primaryCategoryIDPathField;
            }
            set
            {
                this.primaryCategoryIDPathField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemStorefront Storefront
        {
            get
            {
                return this.storefrontField;
            }
            set
            {
                this.storefrontField = value;
            }
        }

        /// <remarks/>
        public string Country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemReturnPolicy ReturnPolicy
        {
            get
            {
                return this.returnPolicyField;
            }
            set
            {
                this.returnPolicyField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemMinimumToBid MinimumToBid
        {
            get
            {
                return this.minimumToBidField;
            }
            set
            {
                this.minimumToBidField = value;
            }
        }

        /// <remarks/>
        public GetMultipleItemsResponseItemProductID ProductID
        {
            get
            {
                return this.productIDField;
            }
            set
            {
                this.productIDField = value;
            }
        }

        /// <remarks/>
        public bool AutoPay
        {
            get
            {
                return this.autoPayField;
            }
            set
            {
                this.autoPayField = value;
            }
        }

        /// <remarks/>
        public bool IntegratedMerchantCreditCardEnabled
        {
            get
            {
                return this.integratedMerchantCreditCardEnabledField;
            }
            set
            {
                this.integratedMerchantCreditCardEnabledField = value;
            }
        }

        /// <remarks/>
        public int HandlingTime
        {
            get
            {
                return this.handlingTimeField;
            }
            set
            {
                this.handlingTimeField = value;
            }
        }

        /// <remarks/>
        public bool TopRatedListing
        {
            get
            {
                return this.topRatedListingField;
            }
            set
            {
                this.topRatedListingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopRatedListingSpecified
        {
            get
            {
                return this.topRatedListingFieldSpecified;
            }
            set
            {
                this.topRatedListingFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool GlobalShipping
        {
            get
            {
                return this.globalShippingField;
            }
            set
            {
                this.globalShippingField = value;
            }
        }

        /// <remarks/>
        public int QuantitySoldByPickupInStore
        {
            get
            {
                return this.quantitySoldByPickupInStoreField;
            }
            set
            {
                this.quantitySoldByPickupInStoreField = value;
            }
        }

        /// <remarks/>
        public bool NewBestOffer
        {
            get
            {
                return this.newBestOfferField;
            }
            set
            {
                this.newBestOfferField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
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
                return this.userIDField;
            }
            set
            {
                this.userIDField = value;
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

        /// <remarks/>
        public decimal PositiveFeedbackPercent
        {
            get
            {
                return this.positiveFeedbackPercentField;
            }
            set
            {
                this.positiveFeedbackPercentField = value;
            }
        }

        /// <remarks/>
        public bool TopRatedSeller
        {
            get
            {
                return this.topRatedSellerField;
            }
            set
            {
                this.topRatedSellerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TopRatedSellerSpecified
        {
            get
            {
                return this.topRatedSellerFieldSpecified;
            }
            set
            {
                this.topRatedSellerFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
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
                return this.currencyIDField;
            }
            set
            {
                this.currencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public partial class GetMultipleItemsResponseItemCurrentPrice
    {

        private string currencyIDField;

        private decimal valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string currencyID
        {
            get
            {
                return this.currencyIDField;
            }
            set
            {
                this.currencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
    public partial class GetMultipleItemsResponseItemStorefront
    {

        private string storeURLField;

        private string storeNameField;

        /// <remarks/>
        public string StoreURL
        {
            get
            {
                return this.storeURLField;
            }
            set
            {
                this.storeURLField = value;
            }
        }

        /// <remarks/>
        public string StoreName
        {
            get
            {
                return this.storeNameField;
            }
            set
            {
                this.storeNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
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
                return this.refundField;
            }
            set
            {
                this.refundField = value;
            }
        }

        /// <remarks/>
        public string ReturnsWithin
        {
            get
            {
                return this.returnsWithinField;
            }
            set
            {
                this.returnsWithinField = value;
            }
        }

        /// <remarks/>
        public string ReturnsAccepted
        {
            get
            {
                return this.returnsAcceptedField;
            }
            set
            {
                this.returnsAcceptedField = value;
            }
        }

        /// <remarks/>
        public string ShippingCostPaidBy
        {
            get
            {
                return this.shippingCostPaidByField;
            }
            set
            {
                this.shippingCostPaidByField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
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
                return this.currencyIDField;
            }
            set
            {
                this.currencyIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
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
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:ebay:apis:eBLBaseComponents")]
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
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public uint Value
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


}
