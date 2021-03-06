﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("AdventureWorks2012Model1", "FK_Product_ProductModel_ProductModelID", "ProductModel", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(SimpleCrud.ProductModel), "Product", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(SimpleCrud.Product), true)]

#endregion

namespace SimpleCrud
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class AdventureWorks2012Entities1 : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new AdventureWorks2012Entities1 object using the connection string found in the 'AdventureWorks2012Entities1' section of the application configuration file.
        /// </summary>
        public AdventureWorks2012Entities1() : base("name=AdventureWorks2012Entities1", "AdventureWorks2012Entities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AdventureWorks2012Entities1 object.
        /// </summary>
        public AdventureWorks2012Entities1(string connectionString) : base(connectionString, "AdventureWorks2012Entities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AdventureWorks2012Entities1 object.
        /// </summary>
        public AdventureWorks2012Entities1(EntityConnection connection) : base(connection, "AdventureWorks2012Entities1")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Product> Products
        {
            get
            {
                if ((_Products == null))
                {
                    _Products = base.CreateObjectSet<Product>("Products");
                }
                return _Products;
            }
        }
        private ObjectSet<Product> _Products;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<ProductModel> ProductModels
        {
            get
            {
                if ((_ProductModels == null))
                {
                    _ProductModels = base.CreateObjectSet<ProductModel>("ProductModels");
                }
                return _ProductModels;
            }
        }
        private ObjectSet<ProductModel> _ProductModels;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Products EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToProducts(Product product)
        {
            base.AddObject("Products", product);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the ProductModels EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToProductModels(ProductModel productModel)
        {
            base.AddObject("ProductModels", productModel);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="AdventureWorks2012Model1", Name="Product")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Product : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Product object.
        /// </summary>
        /// <param name="productID">Initial value of the ProductID property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="productNumber">Initial value of the ProductNumber property.</param>
        /// <param name="makeFlag">Initial value of the MakeFlag property.</param>
        /// <param name="finishedGoodsFlag">Initial value of the FinishedGoodsFlag property.</param>
        /// <param name="safetyStockLevel">Initial value of the SafetyStockLevel property.</param>
        /// <param name="reorderPoint">Initial value of the ReorderPoint property.</param>
        /// <param name="standardCost">Initial value of the StandardCost property.</param>
        /// <param name="listPrice">Initial value of the ListPrice property.</param>
        /// <param name="daysToManufacture">Initial value of the DaysToManufacture property.</param>
        /// <param name="sellStartDate">Initial value of the SellStartDate property.</param>
        /// <param name="rowguid">Initial value of the rowguid property.</param>
        /// <param name="modifiedDate">Initial value of the ModifiedDate property.</param>
        public static Product CreateProduct(global::System.Int32 productID, global::System.String name, global::System.String productNumber, global::System.Boolean makeFlag, global::System.Boolean finishedGoodsFlag, global::System.Int16 safetyStockLevel, global::System.Int16 reorderPoint, global::System.Decimal standardCost, global::System.Decimal listPrice, global::System.Int32 daysToManufacture, global::System.DateTime sellStartDate, global::System.Guid rowguid, global::System.DateTime modifiedDate)
        {
            Product product = new Product();
            product.ProductID = productID;
            product.Name = name;
            product.ProductNumber = productNumber;
            product.MakeFlag = makeFlag;
            product.FinishedGoodsFlag = finishedGoodsFlag;
            product.SafetyStockLevel = safetyStockLevel;
            product.ReorderPoint = reorderPoint;
            product.StandardCost = standardCost;
            product.ListPrice = listPrice;
            product.DaysToManufacture = daysToManufacture;
            product.SellStartDate = sellStartDate;
            product.rowguid = rowguid;
            product.ModifiedDate = modifiedDate;
            return product;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProductID
        {
            get
            {
                return _ProductID;
            }
            set
            {
                if (_ProductID != value)
                {
                    OnProductIDChanging(value);
                    ReportPropertyChanging("ProductID");
                    _ProductID = StructuralObject.SetValidValue(value, "ProductID");
                    ReportPropertyChanged("ProductID");
                    OnProductIDChanged();
                }
            }
        }
        private global::System.Int32 _ProductID;
        partial void OnProductIDChanging(global::System.Int32 value);
        partial void OnProductIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false, "Name");
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String ProductNumber
        {
            get
            {
                return _ProductNumber;
            }
            set
            {
                OnProductNumberChanging(value);
                ReportPropertyChanging("ProductNumber");
                _ProductNumber = StructuralObject.SetValidValue(value, false, "ProductNumber");
                ReportPropertyChanged("ProductNumber");
                OnProductNumberChanged();
            }
        }
        private global::System.String _ProductNumber;
        partial void OnProductNumberChanging(global::System.String value);
        partial void OnProductNumberChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean MakeFlag
        {
            get
            {
                return _MakeFlag;
            }
            set
            {
                OnMakeFlagChanging(value);
                ReportPropertyChanging("MakeFlag");
                _MakeFlag = StructuralObject.SetValidValue(value, "MakeFlag");
                ReportPropertyChanged("MakeFlag");
                OnMakeFlagChanged();
            }
        }
        private global::System.Boolean _MakeFlag;
        partial void OnMakeFlagChanging(global::System.Boolean value);
        partial void OnMakeFlagChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Boolean FinishedGoodsFlag
        {
            get
            {
                return _FinishedGoodsFlag;
            }
            set
            {
                OnFinishedGoodsFlagChanging(value);
                ReportPropertyChanging("FinishedGoodsFlag");
                _FinishedGoodsFlag = StructuralObject.SetValidValue(value, "FinishedGoodsFlag");
                ReportPropertyChanged("FinishedGoodsFlag");
                OnFinishedGoodsFlagChanged();
            }
        }
        private global::System.Boolean _FinishedGoodsFlag;
        partial void OnFinishedGoodsFlagChanging(global::System.Boolean value);
        partial void OnFinishedGoodsFlagChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Color
        {
            get
            {
                return _Color;
            }
            set
            {
                OnColorChanging(value);
                ReportPropertyChanging("Color");
                _Color = StructuralObject.SetValidValue(value, true, "Color");
                ReportPropertyChanged("Color");
                OnColorChanged();
            }
        }
        private global::System.String _Color;
        partial void OnColorChanging(global::System.String value);
        partial void OnColorChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 SafetyStockLevel
        {
            get
            {
                return _SafetyStockLevel;
            }
            set
            {
                OnSafetyStockLevelChanging(value);
                ReportPropertyChanging("SafetyStockLevel");
                _SafetyStockLevel = StructuralObject.SetValidValue(value, "SafetyStockLevel");
                ReportPropertyChanged("SafetyStockLevel");
                OnSafetyStockLevelChanged();
            }
        }
        private global::System.Int16 _SafetyStockLevel;
        partial void OnSafetyStockLevelChanging(global::System.Int16 value);
        partial void OnSafetyStockLevelChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int16 ReorderPoint
        {
            get
            {
                return _ReorderPoint;
            }
            set
            {
                OnReorderPointChanging(value);
                ReportPropertyChanging("ReorderPoint");
                _ReorderPoint = StructuralObject.SetValidValue(value, "ReorderPoint");
                ReportPropertyChanged("ReorderPoint");
                OnReorderPointChanged();
            }
        }
        private global::System.Int16 _ReorderPoint;
        partial void OnReorderPointChanging(global::System.Int16 value);
        partial void OnReorderPointChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal StandardCost
        {
            get
            {
                return _StandardCost;
            }
            set
            {
                OnStandardCostChanging(value);
                ReportPropertyChanging("StandardCost");
                _StandardCost = StructuralObject.SetValidValue(value, "StandardCost");
                ReportPropertyChanged("StandardCost");
                OnStandardCostChanged();
            }
        }
        private global::System.Decimal _StandardCost;
        partial void OnStandardCostChanging(global::System.Decimal value);
        partial void OnStandardCostChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Decimal ListPrice
        {
            get
            {
                return _ListPrice;
            }
            set
            {
                OnListPriceChanging(value);
                ReportPropertyChanging("ListPrice");
                _ListPrice = StructuralObject.SetValidValue(value, "ListPrice");
                ReportPropertyChanged("ListPrice");
                OnListPriceChanged();
            }
        }
        private global::System.Decimal _ListPrice;
        partial void OnListPriceChanging(global::System.Decimal value);
        partial void OnListPriceChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Size
        {
            get
            {
                return _Size;
            }
            set
            {
                OnSizeChanging(value);
                ReportPropertyChanging("Size");
                _Size = StructuralObject.SetValidValue(value, true, "Size");
                ReportPropertyChanged("Size");
                OnSizeChanged();
            }
        }
        private global::System.String _Size;
        partial void OnSizeChanging(global::System.String value);
        partial void OnSizeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SizeUnitMeasureCode
        {
            get
            {
                return _SizeUnitMeasureCode;
            }
            set
            {
                OnSizeUnitMeasureCodeChanging(value);
                ReportPropertyChanging("SizeUnitMeasureCode");
                _SizeUnitMeasureCode = StructuralObject.SetValidValue(value, true, "SizeUnitMeasureCode");
                ReportPropertyChanged("SizeUnitMeasureCode");
                OnSizeUnitMeasureCodeChanged();
            }
        }
        private global::System.String _SizeUnitMeasureCode;
        partial void OnSizeUnitMeasureCodeChanging(global::System.String value);
        partial void OnSizeUnitMeasureCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String WeightUnitMeasureCode
        {
            get
            {
                return _WeightUnitMeasureCode;
            }
            set
            {
                OnWeightUnitMeasureCodeChanging(value);
                ReportPropertyChanging("WeightUnitMeasureCode");
                _WeightUnitMeasureCode = StructuralObject.SetValidValue(value, true, "WeightUnitMeasureCode");
                ReportPropertyChanged("WeightUnitMeasureCode");
                OnWeightUnitMeasureCodeChanged();
            }
        }
        private global::System.String _WeightUnitMeasureCode;
        partial void OnWeightUnitMeasureCodeChanging(global::System.String value);
        partial void OnWeightUnitMeasureCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                OnWeightChanging(value);
                ReportPropertyChanging("Weight");
                _Weight = StructuralObject.SetValidValue(value, "Weight");
                ReportPropertyChanged("Weight");
                OnWeightChanged();
            }
        }
        private Nullable<global::System.Decimal> _Weight;
        partial void OnWeightChanging(Nullable<global::System.Decimal> value);
        partial void OnWeightChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 DaysToManufacture
        {
            get
            {
                return _DaysToManufacture;
            }
            set
            {
                OnDaysToManufactureChanging(value);
                ReportPropertyChanging("DaysToManufacture");
                _DaysToManufacture = StructuralObject.SetValidValue(value, "DaysToManufacture");
                ReportPropertyChanged("DaysToManufacture");
                OnDaysToManufactureChanged();
            }
        }
        private global::System.Int32 _DaysToManufacture;
        partial void OnDaysToManufactureChanging(global::System.Int32 value);
        partial void OnDaysToManufactureChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ProductLine
        {
            get
            {
                return _ProductLine;
            }
            set
            {
                OnProductLineChanging(value);
                ReportPropertyChanging("ProductLine");
                _ProductLine = StructuralObject.SetValidValue(value, true, "ProductLine");
                ReportPropertyChanged("ProductLine");
                OnProductLineChanged();
            }
        }
        private global::System.String _ProductLine;
        partial void OnProductLineChanging(global::System.String value);
        partial void OnProductLineChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Class
        {
            get
            {
                return _Class;
            }
            set
            {
                OnClassChanging(value);
                ReportPropertyChanging("Class");
                _Class = StructuralObject.SetValidValue(value, true, "Class");
                ReportPropertyChanged("Class");
                OnClassChanged();
            }
        }
        private global::System.String _Class;
        partial void OnClassChanging(global::System.String value);
        partial void OnClassChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Style
        {
            get
            {
                return _Style;
            }
            set
            {
                OnStyleChanging(value);
                ReportPropertyChanging("Style");
                _Style = StructuralObject.SetValidValue(value, true, "Style");
                ReportPropertyChanged("Style");
                OnStyleChanged();
            }
        }
        private global::System.String _Style;
        partial void OnStyleChanging(global::System.String value);
        partial void OnStyleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ProductSubcategoryID
        {
            get
            {
                return _ProductSubcategoryID;
            }
            set
            {
                OnProductSubcategoryIDChanging(value);
                ReportPropertyChanging("ProductSubcategoryID");
                _ProductSubcategoryID = StructuralObject.SetValidValue(value, "ProductSubcategoryID");
                ReportPropertyChanged("ProductSubcategoryID");
                OnProductSubcategoryIDChanged();
            }
        }
        private Nullable<global::System.Int32> _ProductSubcategoryID;
        partial void OnProductSubcategoryIDChanging(Nullable<global::System.Int32> value);
        partial void OnProductSubcategoryIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ProductModelID
        {
            get
            {
                return _ProductModelID;
            }
            set
            {
                OnProductModelIDChanging(value);
                ReportPropertyChanging("ProductModelID");
                _ProductModelID = StructuralObject.SetValidValue(value, "ProductModelID");
                ReportPropertyChanged("ProductModelID");
                OnProductModelIDChanged();
            }
        }
        private Nullable<global::System.Int32> _ProductModelID;
        partial void OnProductModelIDChanging(Nullable<global::System.Int32> value);
        partial void OnProductModelIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime SellStartDate
        {
            get
            {
                return _SellStartDate;
            }
            set
            {
                OnSellStartDateChanging(value);
                ReportPropertyChanging("SellStartDate");
                _SellStartDate = StructuralObject.SetValidValue(value, "SellStartDate");
                ReportPropertyChanged("SellStartDate");
                OnSellStartDateChanged();
            }
        }
        private global::System.DateTime _SellStartDate;
        partial void OnSellStartDateChanging(global::System.DateTime value);
        partial void OnSellStartDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> SellEndDate
        {
            get
            {
                return _SellEndDate;
            }
            set
            {
                OnSellEndDateChanging(value);
                ReportPropertyChanging("SellEndDate");
                _SellEndDate = StructuralObject.SetValidValue(value, "SellEndDate");
                ReportPropertyChanged("SellEndDate");
                OnSellEndDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _SellEndDate;
        partial void OnSellEndDateChanging(Nullable<global::System.DateTime> value);
        partial void OnSellEndDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> DiscontinuedDate
        {
            get
            {
                return _DiscontinuedDate;
            }
            set
            {
                OnDiscontinuedDateChanging(value);
                ReportPropertyChanging("DiscontinuedDate");
                _DiscontinuedDate = StructuralObject.SetValidValue(value, "DiscontinuedDate");
                ReportPropertyChanged("DiscontinuedDate");
                OnDiscontinuedDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _DiscontinuedDate;
        partial void OnDiscontinuedDateChanging(Nullable<global::System.DateTime> value);
        partial void OnDiscontinuedDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid rowguid
        {
            get
            {
                return _rowguid;
            }
            set
            {
                OnrowguidChanging(value);
                ReportPropertyChanging("rowguid");
                _rowguid = StructuralObject.SetValidValue(value, "rowguid");
                ReportPropertyChanged("rowguid");
                OnrowguidChanged();
            }
        }
        private global::System.Guid _rowguid;
        partial void OnrowguidChanging(global::System.Guid value);
        partial void OnrowguidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                OnModifiedDateChanging(value);
                ReportPropertyChanging("ModifiedDate");
                _ModifiedDate = StructuralObject.SetValidValue(value, "ModifiedDate");
                ReportPropertyChanged("ModifiedDate");
                OnModifiedDateChanged();
            }
        }
        private global::System.DateTime _ModifiedDate;
        partial void OnModifiedDateChanging(global::System.DateTime value);
        partial void OnModifiedDateChanged();

        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("AdventureWorks2012Model1", "FK_Product_ProductModel_ProductModelID", "ProductModel")]
        public ProductModel ProductModel
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ProductModel>("AdventureWorks2012Model1.FK_Product_ProductModel_ProductModelID", "ProductModel").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ProductModel>("AdventureWorks2012Model1.FK_Product_ProductModel_ProductModelID", "ProductModel").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ProductModel> ProductModelReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ProductModel>("AdventureWorks2012Model1.FK_Product_ProductModel_ProductModelID", "ProductModel");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ProductModel>("AdventureWorks2012Model1.FK_Product_ProductModel_ProductModelID", "ProductModel", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="AdventureWorks2012Model1", Name="ProductModel")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class ProductModel : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new ProductModel object.
        /// </summary>
        /// <param name="productModelID">Initial value of the ProductModelID property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="rowguid">Initial value of the rowguid property.</param>
        /// <param name="modifiedDate">Initial value of the ModifiedDate property.</param>
        public static ProductModel CreateProductModel(global::System.Int32 productModelID, global::System.String name, global::System.Guid rowguid, global::System.DateTime modifiedDate)
        {
            ProductModel productModel = new ProductModel();
            productModel.ProductModelID = productModelID;
            productModel.Name = name;
            productModel.rowguid = rowguid;
            productModel.ModifiedDate = modifiedDate;
            return productModel;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ProductModelID
        {
            get
            {
                return _ProductModelID;
            }
            set
            {
                if (_ProductModelID != value)
                {
                    OnProductModelIDChanging(value);
                    ReportPropertyChanging("ProductModelID");
                    _ProductModelID = StructuralObject.SetValidValue(value, "ProductModelID");
                    ReportPropertyChanged("ProductModelID");
                    OnProductModelIDChanged();
                }
            }
        }
        private global::System.Int32 _ProductModelID;
        partial void OnProductModelIDChanging(global::System.Int32 value);
        partial void OnProductModelIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false, "Name");
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CatalogDescription
        {
            get
            {
                return _CatalogDescription;
            }
            set
            {
                OnCatalogDescriptionChanging(value);
                ReportPropertyChanging("CatalogDescription");
                _CatalogDescription = StructuralObject.SetValidValue(value, true, "CatalogDescription");
                ReportPropertyChanged("CatalogDescription");
                OnCatalogDescriptionChanged();
            }
        }
        private global::System.String _CatalogDescription;
        partial void OnCatalogDescriptionChanging(global::System.String value);
        partial void OnCatalogDescriptionChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Instructions
        {
            get
            {
                return _Instructions;
            }
            set
            {
                OnInstructionsChanging(value);
                ReportPropertyChanging("Instructions");
                _Instructions = StructuralObject.SetValidValue(value, true, "Instructions");
                ReportPropertyChanged("Instructions");
                OnInstructionsChanged();
            }
        }
        private global::System.String _Instructions;
        partial void OnInstructionsChanging(global::System.String value);
        partial void OnInstructionsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid rowguid
        {
            get
            {
                return _rowguid;
            }
            set
            {
                OnrowguidChanging(value);
                ReportPropertyChanging("rowguid");
                _rowguid = StructuralObject.SetValidValue(value, "rowguid");
                ReportPropertyChanged("rowguid");
                OnrowguidChanged();
            }
        }
        private global::System.Guid _rowguid;
        partial void OnrowguidChanging(global::System.Guid value);
        partial void OnrowguidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                OnModifiedDateChanging(value);
                ReportPropertyChanging("ModifiedDate");
                _ModifiedDate = StructuralObject.SetValidValue(value, "ModifiedDate");
                ReportPropertyChanged("ModifiedDate");
                OnModifiedDateChanged();
            }
        }
        private global::System.DateTime _ModifiedDate;
        partial void OnModifiedDateChanging(global::System.DateTime value);
        partial void OnModifiedDateChanged();

        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("AdventureWorks2012Model1", "FK_Product_ProductModel_ProductModelID", "Product")]
        public EntityCollection<Product> Products
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Product>("AdventureWorks2012Model1.FK_Product_ProductModel_ProductModelID", "Product");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Product>("AdventureWorks2012Model1.FK_Product_ProductModel_ProductModelID", "Product", value);
                }
            }
        }

        #endregion

    }

    #endregion

}
