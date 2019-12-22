create function get_sales_by_days(_startdate timestamp without time zone, _finishdate timestamp without time zone)
    returns TABLE(sale_dt timestamp without time zone, sold_count bigint)
    language plpgsql
as
$$
BEGIN
    RETURN QUERY SELECT s.sale_date, count(s.book_id) as sold_count
                 FROM common.sales s
                 --WHERE s.sale_date BETWEEN _startDate AND _finishDate
                 GROUP BY sale_date;
END
$$;

alter function get_sales_by_days(timestamp, timestamp) owner to postgres;

